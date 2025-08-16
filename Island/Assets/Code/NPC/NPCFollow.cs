using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected Animator _animate;
    [SerializeField] private double _walkSpeed;
    [SerializeField] private double _runSpeed;
    [SerializeField] private float _stopRange;

    public GameObject _target;

    private bool _isWalking;
    private bool _isRunning;
    private Vector3 _position;
    private Vector3 _walkRange;
    private Vector3 _runRange;
    private string _currentAnimation = "";
    private string _previousAnimation = "";
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animator>();
    }

    void Update()
    {
        if (_target != null)
        {
            followTarget();
        }

        animationState();
        rangeToPlayer();
    }

    private enum animation_Types
    {
        idle,
        walk,
        run
    }

    private void animationState()
    {
        _previousAnimation = _currentAnimation;
        
        if (_isRunning)
        {
            _currentAnimation = "run";
        }
        else if (_isWalking)
        {
            _currentAnimation = "walk";
        }
        else
        {
            _currentAnimation = "idle";
        }

        if (_currentAnimation != _previousAnimation)
        {
            switch (_currentAnimation)
            {
                case "idle":
                    CrossFadeAnimation("Idle");
                    break;
                    
                case "walk":
                    CrossFadeAnimation("Walking");
                    break;
                    
                case "run":
                    CrossFadeAnimation("Jogging");
                    break;
            }
        }
    }
    
    private void CrossFadeAnimation(string typeOfAnimation)
    {
        if (_animate != null)
        {
            _animate.CrossFade(typeOfAnimation, 0.1f);
        }
    }

    private void followTarget()
    {
        if (_target == null) return;

        Vector3 targetPosition = _target.transform.position;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget <= _stopRange)
        {
            _agent.speed = 0f;
            _isWalking = false;
            _isRunning = false;
            return;
        }
        
        _agent.SetDestination(targetPosition);
        
        if (distanceToTarget > _walkRange.magnitude)
        {
            _isWalking = true;
            _isRunning = false;
            _agent.speed = (float)_walkSpeed;
        }
        else if (distanceToTarget > _runRange.magnitude)
        {
            _isWalking = false;
            _isRunning = true;
            _agent.speed = (float)_runSpeed;
        }
        else
        {
            _isWalking = false;
            _isRunning = false;
            _agent.speed = 0f;
        }
    }

    private void rangeToPlayer()
    {
        if (_target == null) return;
        float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
        if (distanceToTarget <= _stopRange)
        {
            _agent.speed = 0f;
            _isWalking = false;
            _isRunning = false;
        }
    }
}
