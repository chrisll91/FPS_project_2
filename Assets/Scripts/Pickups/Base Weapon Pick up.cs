using UnityEngine;

public abstract class Pickup : MonoBehaviour
{


    [SerializeField] float rotationSpeedx = 100f;
    [SerializeField] float rotationSpeedy = 100f;
    [SerializeField] float rotationSpeedz = 100f;

   

    const string PLAYER_STRING = "Player";

   
    private void Update()
    {
        transform.Rotate(rotationSpeedx * Time.deltaTime, rotationSpeedy * Time.deltaTime, rotationSpeedz * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            Debug.Log("PLAYER TRIGGER ENTERED");
        }
        else
        {
            return;
        }
        activeWeapon activeWeapon = other.GetComponentInChildren<activeWeapon>();
        if (activeWeapon == null)
            Debug.Log("no active weapon found");
        if (activeWeapon.isRealoding) return;

        if (other.CompareTag(PLAYER_STRING))
        {
            OnPickup(activeWeapon);
            Destroy(gameObject);
        }
    }

    
    protected abstract void OnPickup(activeWeapon activeWeapon);
}
