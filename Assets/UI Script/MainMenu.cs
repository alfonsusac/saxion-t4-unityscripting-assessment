using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; // used to manage scenes

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        // Loads Scene of the Game
        // When play button is pressed, load the next scene in the queue
        // Access scene queue from Build Settigns, drag and drop scene to top square.
        SceneManager.LoadScene(1);
        PauseMenu.RequestResume();
        PauseMenu.RequestClearGameOver();
        PlayerScore.ResetScore();
    }
    public static void RetryLevel()
    {
        // load the scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PauseMenu.RequestResume();
        PauseMenu.RequestClearGameOver();
        // to-do reset score to last saved score;
        PlayerScore.RollbackScore();

    }
    public void GoToLevel1()
    {
        SceneManager.LoadScene(2);
        PauseMenu.RequestResume();
        PlayerScore.SafeScore();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }

}
