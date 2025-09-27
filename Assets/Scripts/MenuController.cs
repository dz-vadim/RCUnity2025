using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel, optionPanel;
    private Text bestScoreText;
    private Slider volumeSlider;
    public static int colorNum, bestScore;
    private enum  Colors
    {
        WHITE = 1,
        YELLOW = 2,
        GREEN = 3
    }
    private void Start()
    {
        menuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }
    private void Update()
    {
        //AudioListener.volume = volumeSlider.value;
    }
    public void SetColor(string color)
    {
        switch (color)
        {
            case "WHITE": colorNum = (int)Colors.WHITE; break;
            case "YELLOW": colorNum = (int)Colors.YELLOW; break;
            case "GREEN": colorNum = (int)Colors.GREEN; break;
        }
        PlayerPrefs.SetInt("colorNum", colorNum);
    }
    public void OpenGameLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
