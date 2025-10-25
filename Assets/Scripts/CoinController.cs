using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int coins = 150;
    [SerializeField] TMP_Text coinsText;

    private void Start()
    {
        coinsText.text = coins.ToString();
    }

    public bool SpendCoin(int cost)
    {
        if (coins >= cost)
        {
            coins -= cost;
            coinsText.text = coins.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void EarnCoin(int cost)
    {
        coinsText.text = coins.ToString();
        coins += cost;
    }
}
