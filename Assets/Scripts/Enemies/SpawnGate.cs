using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] int MaxSpawnAmount = 10;
    int spawnAmount = 0;
    


    PlayerHealth player;
    private Coroutine spawnCoroutine;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();

        spawnCoroutine = StartCoroutine(spawnRoutine());

    }

    public void adjustMaxSpawnAmount()
    {
        MaxSpawnAmount++;
    }
    

    public void adjustSpawnAmount(int amount)
    {
        spawnAmount += amount;
        if (spawnAmount < MaxSpawnAmount && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(spawnRoutine());
        }
    }
    
    IEnumerator spawnRoutine()
    {
        
        while (player.currentHealth > 0 && spawnAmount < MaxSpawnAmount)
        {

            GameObject newEnemy = Instantiate(EnemyPrefab, spawnPoint.position, transform.rotation);

            EnemyHealth health = newEnemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.spawnGate = this;
            }

            spawnAmount++;
            yield return new WaitForSeconds(spawnTime);
        }

        spawnCoroutine = null;
    }


}
