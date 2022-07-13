using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{

    // UI Component
    [SerializeField] TextMeshProUGUI scoreText;
    private static int score = 500;
    public static int lastStoredScore = score;
    public static int Score { get { return score; } }
    public static bool reset = true;


    // Start is called before the first frame update
    void Start()
    {
        if (reset)
        {
            reset = false;
            score = 0;
            lastStoredScore = 0;
        }
        if (scoreText == null) Debug.LogError("Score Text Not Found!");
    }

    // Update is called once per frame
    void Update()
    {
        // update UI
        if(scoreText != null) scoreText.text = score + "";
    }

    public static void AddScore(int x)
    {
        score += x;
    }
    public static void ResetScore()
    {
        lastStoredScore = 0;
        score = 0;
    }
    public static void RollbackScore()
    {
        score = lastStoredScore;
    }
    public static void SafeScore()
    {
        lastStoredScore = score;
    }
}
