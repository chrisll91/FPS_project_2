using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SurvivalModeSceneManager : MonoBehaviour
{
    [SerializeField] TMP_Text EnemiesLeftText;
    [SerializeField] TMP_Text EnemiesKilledText;
    [SerializeField] GameObject YouWinText;

    public float WaitForMaxSpawnLimitSeconds = 60f;

    protected int enemiesLeft = 0;
    protected int enemiesKilled = 0;

    protected virtual void Start()
    {
        StartCoroutine(IncreaseMaxSpawnLimitRoutine());
    }

    protected virtual IEnumerator IncreaseMaxSpawnLimitRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(WaitForMaxSpawnLimitSeconds);

            // Preserved exactly as your original line
            SpawnGate[] gates = FindObjectsByType<SpawnGate>(FindObjectsSortMode.None);

            foreach (SpawnGate gate in gates)
            {
                gate.adjustMaxSpawnAmount();
            }
        }
    }

    // Methods that can be overridden by child classes
    public virtual void adjustEnemiesText(int amount)
    {
        enemiesLeft += amount;

        if (EnemiesLeftText != null)
            EnemiesLeftText.text = "Enemies Left : " + enemiesLeft;

        if (enemiesLeft <= 0)
            WinGame();
    }

    public virtual void adjustEnemiesKilledText(int amount)
    {
        enemiesKilled += amount;

        if (EnemiesKilledText != null)
            EnemiesKilledText.text = "Enemies killed: " + enemiesKilled;
    }

    protected virtual void WinGame()
    {
        if (YouWinText != null)
            YouWinText.SetActive(true);

        StarterAssets.StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssets.StarterAssetsInputs>();
        starterAssetsInputs?.SetCursorState(false);
    }

    public virtual void OnPlayerDeath()
    {
        SaveHighscore();
    }

    protected virtual void SaveHighscore()
    {
        const string HIGHSCORE_KEY = "highscore";
        int currentHighscore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);

        if (enemiesKilled > currentHighscore)
        {
            PlayerPrefs.SetInt(HIGHSCORE_KEY, enemiesKilled);
            PlayerPrefs.Save();
        }
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitButton()
    {
        SceneManager.LoadScene(0);
    }
}
