using System.Collections;

using UnityEngine;


public class SpawnRandomPickUp : MonoBehaviour
{
    [SerializeField] GameObject[] PickupPrefabs;
    

    private void Start()
    {
        spawnRandomPickup();
    }

    private void spawnRandomPickup()
    {
        int randomIndex = Random.Range(0, PickupPrefabs.Length);
        GameObject PickupToSpawn = PickupPrefabs[randomIndex];

        GameObject newPickupToSpawn = Instantiate(PickupToSpawn, transform.position,transform.rotation);
    }
}
