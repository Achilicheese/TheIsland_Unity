using UnityEngine;
using UnityEngine.AI;

public class NPCStay : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected Animator _animate;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animator>();
    }
    
    public void Stay()
    {
        if (_agent != null)
        {
            _agent.isStopped = true;
            _agent.ResetPath();
            Debug.Log("NPC is now staying in place");
        }
        
        if (_animate != null)
        {
            _animate.CrossFade("Idle", 0.1f);
            Debug.Log("Setting NPC animation to IDLE");
        }
    }
    
    public void ResumeMovement()
    {
        if (_agent != null)
        {
            _agent.isStopped = false;
            Debug.Log("NPC can now move again");
        }
    }
}
