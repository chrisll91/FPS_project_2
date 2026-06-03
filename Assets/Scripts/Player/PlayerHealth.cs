using Cinemachine;
using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [Range(1,10)]
    [SerializeField] int StartingHealth = 10;
    [SerializeField] Image[] shieldBars;

    [Header("Cameras")]
    [SerializeField] CinemachineVirtualCamera DeathVirtualCamera;
    [SerializeField] Transform weaponCamera;

    [Header("Game over Prefab")]
    [SerializeField] GameObject GameOverContainer;
     

    public int currentHealth;
    int GameOverVirtalCameraPriority = 20;

    public static event Action OnPlayerDied;

    private void Awake()
    {
        currentHealth = StartingHealth;
        adjustShieldUI();
    }
    public void takeDamage(int amount)
    {
        
        currentHealth -= amount;
        adjustShieldUI();

        if (currentHealth <= 0)
        {
            weaponCamera.parent = null;
            DeathVirtualCamera.Priority = GameOverVirtalCameraPriority;
            GameOverContainer.SetActive(true);
            StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
            starterAssetsInputs.SetCursorState(false);

            SurvivalModeSceneManager gm = FindFirstObjectByType<SurvivalModeSceneManager>();
            if (gm != null)
            {
                Debug.Log("Player Died invoke");
                gm.OnPlayerDeath();
                OnPlayerDied?.Invoke();
            }
            Destroy(this.gameObject);

        }
    }
    private void adjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if( i < currentHealth )
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }

    public void healPlayer()
    {
        foreach(var shield in shieldBars)
        {
            shield.gameObject.SetActive(true);
        }
        currentHealth = StartingHealth;
    }

    
}
