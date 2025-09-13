using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject gamePanel, pausePanel; // for storing panels
    public Text startText; // for storing texts
    [SerializeField]
    private Text scoreText, bestScoreText;
    public int score; // for storing the score

    void Start()
    {
        pausePanel.SetActive(false); // make the pause panel inactive at first
        gamePanel.SetActive(true);   // and the game panel active
        score = 0;                   // set score to zero
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score > MenuController.bestScore)
        { // if score is higher than bestScore, update and save it
            MenuController.bestScore = score;
            PlayerPrefs.SetInt("BestScore", MenuController.bestScore);
        }
        // always update the best score text
        bestScoreText.text = "Best Score: " + MenuController.bestScore.ToString();
    }

    public void BackToMenu()
    {   // set Time.timeScale back to normal
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu"); // load the menu scene
    }

    public void SwitchPause()
    {   // toggle the panels' activation
        pausePanel.SetActive(!pausePanel.activeSelf);
        gamePanel.SetActive(!gamePanel.activeSelf);
        switch (Time.timeScale)
        { // change Time.timeScale to the opposite
            case 1:
                Time.timeScale = 0;
                break;
            case 0:
                Time.timeScale = 1;
                break;
        }
    }
}
