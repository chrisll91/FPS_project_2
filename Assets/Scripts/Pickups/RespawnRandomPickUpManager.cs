using UnityEngine;
using System.Collections;

public class RespawnPickUpManager : MonoBehaviour
{

    [SerializeField] GameObject[] PickUpPrefabs;
    [SerializeField] float respawnTime = 30f;

    private GameObject currentPickUp;
    private bool isRespawning = false;
    void Start()
    {
        SpawnRandomPickUp();
        
    }

    private void Update()
    {
        if (currentPickUp == null && !isRespawning)
        {
            StartCoroutine(RespawnCoroutine());
        }

    }

    private void SpawnRandomPickUp()
    {

        int randomIndex = Random.Range(0, PickUpPrefabs.Length);
        GameObject WhichPickupToSpawn = PickUpPrefabs[randomIndex];
        currentPickUp = Instantiate(WhichPickupToSpawn, transform.position, transform.rotation);
        isRespawning = false;
    }

    private IEnumerator RespawnCoroutine()
    {
        isRespawning = true;
        Debug.Log("Start Respawn Routine");
        yield return new WaitForSeconds(respawnTime);

        SpawnRandomPickUp();
    }
}
