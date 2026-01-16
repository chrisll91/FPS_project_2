using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    
    [SerializeField] float rotationSpeedy = 100f;

    const string PLAYER_STRING = "Player";


    private void Update()
    {
        transform.Rotate(0, rotationSpeedy * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                if (playerHealth.currentHealth != 10)
                {

                    playerHealth.healPlayer();
                    //Debug.Log("Player healed!");
                }
                else { return; }
            }

            
            gameObject.SetActive(false);
        }
    }
}
