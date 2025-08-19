using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
    public NavMeshAgent _agent;
    public Animator _animate;
    [SerializeField] private double _walkSpeed;
    [SerializeField] private double _runSpeed;
    [SerializeField] private float _stopRange;
    [SerializeField] private float _walkRange;
    [SerializeField] private float _runRange;

    public GameObject _target;

    private bool _isWalking;
    private bool _isRunning;
    private Vector3 _position;
    private animation_Types _currentAnimation = animation_Types.idle;   // compare current enum
    private animation_Types _previousAnimation = animation_Types.idle;  // with previous enum
    
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
            _currentAnimation = animation_Types.run;
        }
        else if (_isWalking)
        {
            _currentAnimation = animation_Types.walk;
        }
        else
        {
            _currentAnimation = animation_Types.idle;
        }

        if (_currentAnimation != _previousAnimation)
        {
            switch (_currentAnimation)
            {
                case animation_Types.idle:
                    CrossFadeAnimation("Idle");
                    Debug.Log("Setting animation to IDLE");
                    break;
                    
                case animation_Types.walk:
                    CrossFadeAnimation("Walking");
                    Debug.Log("Setting animation to WALK");
                    break;
                    
                case animation_Types.run:
                    CrossFadeAnimation("Jogging");
                    Debug.Log("Setting animation to RUN");
                    break;
            }

            Debug.Log($"Animation changed from {_previousAnimation} to {_currentAnimation}");     
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
        
        Debug.Log($"Distance: {distanceToTarget:F1}, WalkRange {_walkRange}, RunRange{_runRange}");
        
        if (distanceToTarget > _runRange)
        {
            _isWalking = false;
            _isRunning = true;
            _agent.speed = (float)_runSpeed;
            Debug.Log("running distance");
        }
        else if (distanceToTarget > _walkRange)
        {
            _isWalking = true;
            _isRunning = false;
            _agent.speed = (float)_walkSpeed;
            Debug.Log("walking distance");
        }
        else
        {
            _isWalking = false;
            _isRunning = false;
            _agent.speed = 0f;
            Debug.Log("close to player , stopping");
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
