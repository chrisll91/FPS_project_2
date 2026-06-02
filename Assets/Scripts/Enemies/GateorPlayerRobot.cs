using UnityEngine;

public class GateOrPlayerRobot : BaseRobot
{

    // Meant for my wave mode currently on hold probably forever.
    public Collider playerDetectionZone;
    private bool playerInRange = false;
    const string PLAYER_STRING = "Player";

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            playerInRange = true;
        }
           
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            playerInRange = false;

        }
    }

    protected override Transform GetCurrentTarget()
    {
        if (playerInRange && playerTarget != null)
        {
            return playerTarget;

        }

        return gateTarget;
    }

    


}
