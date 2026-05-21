using UnityEngine;

public class RespawnPickUpManager : MonoBehaviour
{

    [SerializeField] GameObject[] PickUpPrefabs;

    private GameObject currentPickUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnRandomPickUp();
    }

    private void SpawnRandomPickUp()
    {

        int randomIndex = Random.Range(0, PickUpPrefabs.Length);
        GameObject WhichPickupToSpawn = PickUpPrefabs[randomIndex];
        GameObject newPickup = Instantiate(WhichPickupToSpawn, transform.position, transform.rotation);

    }
}
