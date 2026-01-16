using System.Collections;

using UnityEngine;

public class RandomGateSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] gatePrefabs;
    [SerializeField] float respawnDelay = 1f;
    

    private GameObject currentGate;
    private bool isRespawning = false;

    private void Start()
    {
        SpawnRandomGate();
    }

    private void Update()
    {
        if(currentGate == null && !isRespawning)
        {
            StartCoroutine(RespawnGate());
        }
    }
    private void SpawnRandomGate()
    {
        
        int randomIndex = Random.Range(0, gatePrefabs.Length);
        GameObject WhichGateToSpawn = gatePrefabs[randomIndex];


        GameObject newGate = Instantiate(WhichGateToSpawn, transform.position, transform.rotation);
        SpawnGate gate = newGate.GetComponent<SpawnGate>();
        EnemyHealth enemyHealth = newGate.GetComponent<EnemyHealth>();
        SurvivalModeSceneManager gm = FindFirstObjectByType<SurvivalModeSceneManager>();

        if (enemyHealth != null)
        {
            enemyHealth.spawnGate = gate;
            enemyHealth.gameManager = gm;
        }
        currentGate = newGate;
        
    }



    private IEnumerator RespawnGate()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnDelay);
        SpawnRandomGate();
        isRespawning = false;
    }
}

