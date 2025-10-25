using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonShopSettings : MonoBehaviour
{
    [SerializeField] private TMP_Text costText;
    [Header("Settings")]
    [SerializeField] private int cost;
    [SerializeField] private int buildIndex;

    private void Awake()
    {
        costText.text = cost.ToString();
        BuildManager manager = FindObjectOfType<BuildManager>();
        GetComponent<Button>().onClick.AddListener(
            () => manager.SetBuildTurret(buildIndex, cost)
            );
    }
}
