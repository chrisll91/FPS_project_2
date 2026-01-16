using UnityEngine;

public class Ammopickup : Pickup
{

    [SerializeField] int ammoAmount = 100;
    protected override void OnPickup(activeWeapon activeWeapon)
    {
        
        //activeWeapon.AdjustAmmo(ammoAmount); enable this if i want the ammo pick up to refill the current weapon magazine aswell
        activeWeapon.adjustAmmoReserve(ammoAmount);
    }
}
