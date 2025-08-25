using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWandering : MonoBehaviour
{
    private float wanderSpeed = 0.3f;
    private float detectionSpeed = 0.6f;
    private float detectionRange = 10f;
    private float stopingDistance = 1.0f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = wanderSpeed;
        agent.stoppingDistance = stopingDistance;
    }

    void Update()
    {
        if (FindTarget())
        {
            return;
        }
        else
        {
            Wander();
        }
    }

    private bool FindTarget()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider obj in nearby)
        {
            // Only check for CapsuleCollider component
            if (obj.TryGetComponent<CapsuleCollider>(out _))
            {
                agent.SetDestination(obj.transform.position);
                agent.speed = detectionSpeed;
                return true;
            }
        }
        agent.speed = wanderSpeed;
        return false;
    }

    private void Wander()
    {
        if (agent.hasPath && agent.remainingDistance < 0.5f)
            return;

        Vector3 randomSpot = transform.position + Random.insideUnitSphere * detectionRange;

        if (NavMesh.SamplePosition(randomSpot, out NavMeshHit hit, detectionRange, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}