using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {   // check if the food touches any part of the snake
        // the tag "Snake" should be on the tail and head prefabs
        if (col.gameObject.tag == "Snake")
        {
            Destroy(gameObject); // if yes, destroy the food
        }
    }
}
