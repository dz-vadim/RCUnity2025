using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private MobileControll instance;

    private void Start()
    {
        instance = FindObjectOfType<MobileControll>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        instance.moveRight = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        instance.moveRight = false;
    }
}
