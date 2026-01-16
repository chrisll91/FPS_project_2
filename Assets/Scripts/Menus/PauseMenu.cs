using StarterAssets;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    
    public GameObject pauseScreen;
    StarterAssetsInputs StarterAssetsInputs;

    private void Awake()
    {
        StarterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
    }




    public void TogglePause()
    {
        

        if (StarterAssetsInputs.Pause)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log(StarterAssetsInputs.Pause);
            return;

        }
        if(!StarterAssetsInputs.Pause) 
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log(StarterAssetsInputs.Pause);
            return;

        }

        
    }
}
