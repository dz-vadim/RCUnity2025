using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController audioInstance;
    void Awake()
    {
        DontDestroyOnLoad(this); // call this method right away
        if (audioInstance == null)
        { // check if the variable has an object
            audioInstance = this; // if not, set it to this object
        }
        else
        { // otherwise, destroy this object
            Destroy(gameObject);
        }
    }
}
