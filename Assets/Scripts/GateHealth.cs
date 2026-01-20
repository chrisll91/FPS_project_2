using UnityEngine;

public class GateHealth : MonoBehaviour
{
    public int MaxGateHealth = 200;
    public int CurrentGateHealth = 200;

    public void GateTakeDamage()
    {
        CurrentGateHealth -= 10;
        if(CurrentGateHealth < 0) CurrentGateHealth = 0;
    }
    public void GateHeal(int heal)
    {
        CurrentGateHealth += heal;
        if( CurrentGateHealth > MaxGateHealth) CurrentGateHealth = MaxGateHealth;
    }
    public bool isDestroyed()
    {
        return CurrentGateHealth <= 0;
    }
}