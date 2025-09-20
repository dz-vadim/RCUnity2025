using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snake"))
        {
            Destroy(gameObject);
        }
    }
}
