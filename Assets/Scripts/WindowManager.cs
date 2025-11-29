using System;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Layout;
    [SerializeField] private GameObject[] windows;
    private void Awake()
    {
        Layout = this;
        foreach (GameObject window in windows)
        {
            window.SetActive(false);
        }
    }
    public void OpenLayout(string name)
    {
        foreach (GameObject window in windows)
        {
            window.SetActive(name == window.name);
        }
    }

    private void Start()
    {
        OpenLayout("Loading");
    }
}
