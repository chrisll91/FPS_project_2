using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int totalWaves = 5;
    public int currentWave = 0;

    public WaveState state = WaveState.Idle;

    private int enemiesAlive = 0;

    private void OnEnable()
    {
        EnemyEvents.OnEnemySpawned += HandleEnemySpawned;
        EnemyEvents.OnEnemyDied += HandleEnemyDied;
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemySpawned -= HandleEnemySpawned;
        EnemyEvents.OnEnemyDied -= HandleEnemyDied;
    }

    private void HandleEnemySpawned()
    {
        if (state == WaveState.InProgress)
            enemiesAlive++;
    }

    private void HandleEnemyDied()
    {
        if (state != WaveState.InProgress)
            return;

        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            EndWave();
        }
    }

    public void StartWave()
    {
        if (state != WaveState.Idle) return;
        if (currentWave >= totalWaves) return;

        currentWave++;
        state = WaveState.InProgress;
        enemiesAlive = 0;

        Debug.Log("Wave started: " + currentWave);
    }

    public void EndWave()
    {
        if (state != WaveState.InProgress) return;

        state = WaveState.Idle;
        Debug.Log("Wave ended: " + currentWave);
    }
}
