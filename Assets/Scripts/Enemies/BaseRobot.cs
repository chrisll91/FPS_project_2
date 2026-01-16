using UnityEngine;
using UnityEngine.AI;

public abstract class BaseRobot : MonoBehaviour
{
    public int scoreValue = 10;
    public EnemyHealth enemyHealth;
    public NavMeshAgent agent;
    public Transform gateTarget;
    public Transform playerTarget;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();

        if (gateTarget == null)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void Update()
    {
        if (agent != null && GetCurrentTarget() != null)
        {
            agent.SetDestination(GetCurrentTarget().position);
        }
    }

    protected abstract Transform GetCurrentTarget();
}
