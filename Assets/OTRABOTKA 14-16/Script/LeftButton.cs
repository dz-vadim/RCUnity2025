using UnityEngine;
using UnityEngine.EventSystems;
public class LeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private MobileControll instance;

    private void Start()
    {
        instance = FindObjectOfType<MobileControll>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        instance.moveLeft = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        instance.moveLeft = false;
    }
}