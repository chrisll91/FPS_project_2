using UnityEngine;

public class Explosion : MonoBehaviour
{
    float radius = 1.75f;
    
    int VSgateExplosionDamage = 10;
    


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
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.takeDamage(SurvivalModeSceneManager.GlobalExplosionDamage);

            }

            GateHealth gateHealth = hitCollider.GetComponent<GateHealth>();
            if (gateHealth != null)
            {
                gateHealth.GateTakeDamage(VSgateExplosionDamage);
            }
        }

    }
}

