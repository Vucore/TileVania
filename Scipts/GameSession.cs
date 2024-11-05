using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLive = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;
    void Awake()   //singleton
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if(numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = playerLive.ToString();
        scoreText.text = score.ToString();
    }
    public void AddToScore(int coinScore)
    {
        score += coinScore;
        scoreText.text = score.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if(playerLive > 1)
        {
            TakeLive();
        }
        else
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {
        
        FindObjectOfType<GamePersist>().ResetGamePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLive()
    {
        playerLive--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLive.ToString();
    }
}

