using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel, optionsPanel;
    private Text bestScoreText;
    private Slider volumeSlider;
    // current color number, best score
    public static int colorNum, bestScore;
    private enum Colors
    {
        WHITE = 1,
        YELLOW = 2,
        GREEN = 3
    }

    void Start()
    {
        bestScoreText = GameObject.Find("BestScoreText").GetComponent<Text>();
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        colorNum = PlayerPrefs.GetInt("ColorN", 1);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }
    void Update()
    {
        bestScoreText.text = "Best Score: " + bestScore.ToString();
        AudioListener.volume = volumeSlider.value; // set the volume
    }

    public void SetColor(string colorSpriteName)
    {
        switch (colorSpriteName)
        {
            case "WHITE":
                colorNum = (int)Colors.WHITE;
                break;
            case "YELLOW":
                colorNum = (int)Colors.YELLOW;
                break;
            case "GREEN":
                colorNum = (int)Colors.GREEN;
                break;
        } // remember the color number, it will be used later to load the correct one
        PlayerPrefs.SetInt("ColorN", colorNum);
    }

    public void OpenGameLevel() // used for the PlayButton
    {
        SceneManager.LoadScene("Game");
    }

    public void SwitchOptions() // used for the OptionsButton and BackButton
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value); // save the slider value
    }

    public void ResetScore() // used for the ResetButton
    {
        PlayerPrefs.SetInt("BestScore", 0);
        bestScore = 0;
    }

    public void QuitGame() // used for the QuitButton
    {
        Application.Quit();
    }
}
