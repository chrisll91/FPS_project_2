using UnityEngine;

public class Ammopickup : MonoBehaviour
{

    [SerializeField] int ammoAmount = 100;
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
            Debug.Log("PLAYER TRIGGER ENTERED");
        }
        else
        {
            return;
        }

        activeWeapon activeWeapon = other.GetComponentInChildren<activeWeapon>();

        if (activeWeapon != null && activeWeapon.currentWeaponSO.reserveAmmo < activeWeapon.currentWeaponSO.totalAmmoCapacity)
        {               
            
            activeWeapon.adjustAmmoReserve(ammoAmount);           
            Destroy(gameObject);
        }
    }
}
