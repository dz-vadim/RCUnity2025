using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawn : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    private void Start()
    {
        if(Random.Range(0,100) > 50)
        {
            Destroy(heart);
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(-4f, 0f);
    }
}
