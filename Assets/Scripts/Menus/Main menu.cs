using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject highscoreCanvas;
    [SerializeField] private GameObject ControlsCanvas;
    [SerializeField] private GameObject[] instructionCanvas;

    int currentInstructionsCanvas = 0;
    


    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);      
        highscoreCanvas.SetActive(false);
        ControlsCanvas.SetActive(false);

    }

    public void tutorialAndNextButton()
    {
        mainMenuCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        ControlsCanvas.SetActive(false);
        handleInstructionsCanvas();
    }

    private void handleInstructionsCanvas()
    {
        
        foreach (var canvas in instructionCanvas)
        {
            canvas.SetActive(false);
        }  
        if (currentInstructionsCanvas >= instructionCanvas.Length)
        {
            currentInstructionsCanvas = 0;
            ShowMainMenu();
            return;
        }
        instructionCanvas[currentInstructionsCanvas].SetActive(true);
        currentInstructionsCanvas++;
    }


    public  void ShowHighscoreCanvas()
    {

        mainMenuCanvas.SetActive(false);
        highscoreCanvas.SetActive(true);
        ControlsCanvas.SetActive(false);
    }

    public void ShowControlsCanvas()
    {

        mainMenuCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        ControlsCanvas.SetActive(true);

    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quiting game");
    }
    public void StartTutorialButton()
    {
       
        SceneManager.LoadScene(1);
        
    }
    public void SurvivalButton()
    {
        SceneManager.LoadScene(2);
        
    }

    
    
    
}
