using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System;
using UnityEditor;

public class SurvivalModeSceneManager : MonoBehaviour
{
    [SerializeField] TMP_Text EnemiesLeftText;
    [SerializeField] TMP_Text EnemiesKilledText;
    [SerializeField] GameObject YouWinText;

    public float WaitForMaxSpawnLimitSeconds = 30f;
    public static int GlobalExplosionDamage = 1;
    protected int enemiesLeft = 0;
    protected int enemiesKilled = 0;

    protected virtual void Start()
    {
        GlobalExplosionDamage = 1;
        StartCoroutine(IncreaseMaxSpawnLimitRoutine());   
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(0);
        }
    }
    protected virtual IEnumerator IncreaseMaxSpawnLimitRoutine()
    {

        // Go through each gate in the level and increase its maximum amount of enemies to be spawn over time.
        while (true)
        {
            yield return new WaitForSeconds(WaitForMaxSpawnLimitSeconds);
            SpawnGate[] gates = FindObjectsByType<SpawnGate>(FindObjectsSortMode.None);

            foreach (SpawnGate gate in gates)
            {
                gate.adjustMaxSpawnAmount();
            }
        }
    }

    public virtual void adjustEnemiesText(int amount)
    {
        enemiesLeft += amount;

        if (EnemiesLeftText != null)
        {
            EnemiesLeftText.text = "Enemies Left : " + enemiesLeft;
        }

        //only meant for tutorial and eventually wave mode (on hold indefinitely)
        if (enemiesLeft <= 0)
        {
            WinGame();
        }
    }

    public virtual void adjustEnemiesKilledText(int amount)
    {
        enemiesKilled += amount;

        if (EnemiesKilledText != null)
        {
            EnemiesKilledText.text = "Enemies killed: " + enemiesKilled;
        }

        if (enemiesKilled % 25 == 0)
        {
            GlobalExplosionDamage++;
            
        }
    }

    // Only For The Tutorial
    protected virtual void WinGame()
    {
        if (YouWinText != null)
            YouWinText.SetActive(true);

        StarterAssets.StarterAssetsInputs starterAssetsInputs = 
            FindFirstObjectByType<StarterAssets.StarterAssetsInputs>();
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
