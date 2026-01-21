using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 5.5f;
    [SerializeField] int VSplayerExplosionDamage = 4;
    [SerializeField] int VSgateExplosionDamage = 10;

    private void Start()
    {
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        Debug.Log("im alive somewhere");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.takeDamage(VSplayerExplosionDamage);
            }

            GateHealth gateHealth = hitCollider.GetComponent<GateHealth>();
            if (gateHealth != null)
            {
                gateHealth.GateTakeDamage(VSgateExplosionDamage);
            }
        }

    }
}

