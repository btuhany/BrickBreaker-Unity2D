using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public int lives;
    public int score;
    int total = 0;
    int lastTotal = 0;
    public bool gameOver;

    [SerializeField] GameObject gameOverPanel;

    public Text livesText;
    public Text scoreText;  
    public Text totalText;

    public Text GameOverText;

    private void Start()
    {
        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();
        totalText.text = "Total: " + total.ToString();
        
        
        gameOverPanel.GetComponent<CanvasGroup>().alpha = 0;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReStart();
        }
        if ((Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.Escape)) && !(gameOver))
        {
            GameOver();
        }
    }

    public void UpdateLives(int count)
    {
        lives+=count;
        if (lives <0)
        {
            lives = 0;
            GameOver();
            
        }
        livesText.text = "Lives: " + lives.ToString();
    }
    public void UpdateTotal()
    {
        lastTotal = total;
        total += score;
        totalText.text = "Total: " + total.ToString() + "/" + lastTotal.ToString(); ;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }
    public void UpdateScore(int point)
    {
        score += point;
        scoreText.text = "Score: " + score.ToString();
    }


    void GameOver()
    {
        total += score;
        GameOverText.text = "Total Score: " + total; 
        gameOver = true;
        totalText.text = "Total: " + total.ToString();
        gameOverPanel.GetComponent<RectTransform>().localScale = Vector3.one;
        gameOverPanel.GetComponent<CanvasGroup>().DOFade(1, 5f); 
        
    }
    
    public void ReStart()
    {
        SceneManager.LoadScene("GamePlay");

    }
    public void QuitApplication()
    {
        Application.Quit();
    }
   }
