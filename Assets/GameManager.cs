using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject clickButton;
    [SerializeField] private GameObject adsButton;
    [SerializeField] private GameObject goodJob;
    [SerializeField] private Text timeText;
    [SerializeField] private Text needToClickText;
    [SerializeField] private int needToClick;
    [SerializeField] private int secondsToWin = 30;


    private int myClick;
    private int seconds;
    private bool isGame;

    private void Start()
    {
        seconds = 0;
        needToClick = 30;
        clickButton.SetActive(true);
        adsButton.SetActive(false);
        goodJob.SetActive(false);
        UpdateUI();
    }
    private void UpdateUI()
    {
        timeText.text = seconds.ToString();
        needToClickText.text = string.Format("{0}/{1}", myClick, needToClick);
    }

    public void ClickButton()
    {
        if(!isGame)
        {
            myClick++;
            StartGame();
            SetRandomButton();
        }
        else
        {
            myClick++;
            SetRandomButton();
            UpdateUI();
            if (myClick >= needToClick)
            {
                CancelInvoke();
                isGame = !isGame;
                clickButton.SetActive(false);
                adsButton.SetActive(false);
                goodJob.SetActive(true);
            }                       
        }
    }
    public void AdsButton()
    {
        myClick -= 5;
        clickButton.SetActive(true);
        adsButton.SetActive(false);
        UpdateUI();
    }
    public void CloseAd()
    {
        clickButton.SetActive(true);
        adsButton.SetActive(false);
    }
    public void RestartButton()
    {
        clickButton.SetActive(true);
        adsButton.SetActive(false);
        goodJob.SetActive(false);
        seconds = 0;
        myClick = 0;
        StartGame();
    }
    private void StartGame()
    {
        isGame = !isGame;
        InvokeRepeating("myTimer", 0f, 1f);        
    }
    private void myTimer()
    {
        seconds++;
        UpdateUI();
        if (seconds >= secondsToWin)
        {
            Start();
        }
    }
    private void SetRandomButton()
    {
        if(Random.Range(0,100) > 10)
        {
            clickButton.SetActive(true);
            adsButton.SetActive(false);
        }
        else
        {
            clickButton.SetActive(false);
            adsButton.SetActive(true);
        }
    }
}
