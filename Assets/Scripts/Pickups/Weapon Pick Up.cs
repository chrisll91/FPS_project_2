using UnityEngine;

public class WeaponPIckUp : Pickup
{
    [SerializeField] WeaponSO weaponSO;

    protected override void OnPickup(activeWeapon activeWeapon)
    {
        activeWeapon.PickUpWeapon(weaponSO);
    }
}


    
