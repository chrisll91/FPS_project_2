using UnityEngine;

public class RobotImpact : MonoBehaviour
{
    EnemyHealth enemyHealth;

    void Awake()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (enemyHealth == null) return;

        if (other.CompareTag("Player") || other.GetComponent<GateHealth>() != null)
        {
            enemyHealth.selfDestruct();
        }
    }
}
