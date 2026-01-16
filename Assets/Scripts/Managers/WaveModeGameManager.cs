using UnityEngine;

public class WaveModeGameManager : MonoBehaviour
{
    public enum GameState
    {
        paused,
        playing,
        won,
        lost
    }

    public GameState currentGameState = GameState.paused;

    public void SetPaused()
    {
        currentGameState = GameState.paused;
        Time.timeScale = 0f;
    }

    public void SetPlaying()
    {
        currentGameState = GameState.playing;
        Time.timeScale = 1f;
    }

    public void SetWon()
    {
        currentGameState = GameState.won;
        Time.timeScale = 0f;
    }

    public void SetLost()
    {
        currentGameState = GameState.lost;
        Time.timeScale = 0f;
    }

    public bool isPlaying()
    {
        return currentGameState == GameState.playing;
    }

    public bool isPaused()
    {
        return currentGameState == GameState.paused;
    }
}
