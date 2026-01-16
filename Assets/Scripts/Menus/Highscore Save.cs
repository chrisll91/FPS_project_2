using TMPro;
using UnityEngine;

public class HighscoreDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text highscoreText;

    private const string HIGHSCORE_KEY = "highscore";

    private void OnEnable()
    {
        UpdateHighscoreText();
    }

    private void UpdateHighscoreText()
    {
        int highscore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        highscoreText.text = "Highest Kill count: " + highscore.ToString();
    }
}