using StarterAssets;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    private StarterAssetsInputs starterAssetsInputs;

    private bool isPaused = false;

    private void Awake()
    {
        starterAssetsInputs =
            FindFirstObjectByType<StarterAssetsInputs>();

        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (starterAssetsInputs.Pause)
        {
            TogglePause();

            starterAssetsInputs.Pause = false;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseScreen.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.lockState = isPaused
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Debug.Log("Pause toggled");
    }
}