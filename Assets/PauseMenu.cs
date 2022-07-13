using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    static bool gameIsPaused = false;
    static bool gameOver = false;
    public static bool GameIsPaused { get { return gameIsPaused; } }

    public GameObject PauseMenuUI;
    public GameObject GameOverUI;
    public GameObject NextGameUI;


    [SerializeField] bool isPaused;
    [SerializeField] bool isGameOver;
    [SerializeField] float theTimeScale;
    

    // Update is called once per frame
    void Update()
    {
        isPaused = gameIsPaused;
        isGameOver = gameOver;
        theTimeScale = Time.timeScale;
        if (request)
        {
            if (!GameIsPaused)
                Resume();
            else
                Pause();
            request = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
        if (requestGameOver)
        {
            ShowGameOverScreen();
            requestGameOver = false;
        }
        if (requestClearGameOver)
        {
            ClearGameOverScreen();
            requestClearGameOver = false;
        }
        if (requestNextLevelScreen)
        {
            ShowNextLevelScreen();
            requestNextLevelScreen = false;
        }
    }

    static bool request = false;
    public static void RequestResume()
    {
        gameIsPaused = false;
        request = true;
    }
    public static void RequestPause()
    {
        gameIsPaused = true;
        request = true;
    }

    static bool requestGameOver;
    public static void RequestGameOverScreen()
    {
        requestGameOver = true;
        gameOver = true;
    }

    static bool requestClearGameOver;
    public static void RequestClearGameOver()
    {
        requestClearGameOver = false;
        gameOver = false;
    }

    static bool requestNextLevelScreen;
    public static void RequestNextLevelScreen()
    {
        requestNextLevelScreen = true;
    }


    public void Resume()
    {
        Debug.Log("Resumed");
        // do Pause() but opposite
        PauseMenuUI.SetActive(false);
        if(NextGameUI != null) NextGameUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        Debug.Log("Game Paused");
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ShowGameOverScreen()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        GameOverUI.SetActive(true);
        PauseMenuUI.SetActive(false);
    }    
    public void ClearGameOverScreen()
    {
        PauseMenuUI.SetActive(false);
        if (gameIsPaused) Resume();
    }
    public void ShowNextLevelScreen()
    {
        NextGameUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

}
