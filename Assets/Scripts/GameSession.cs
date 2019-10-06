using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject ui;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            ui.SetActive(false);
        } else
        {
            ui.SetActive(true);
        }
    }
    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        StartCoroutine(Respawn());
        livesText.text = playerLives.ToString();

    }

    private void ResetGameSession()
    {
        Time.timeScale = 1f;
        StartCoroutine(Dead());
        Destroy(gameObject);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.5f);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator Dead()
    {
        SceneManager.LoadScene(4);
        yield return new WaitForSeconds(0.5f);
        
    }
}
