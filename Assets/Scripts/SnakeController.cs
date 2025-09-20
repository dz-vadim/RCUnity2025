using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject foodPref, tailPref;
    private GameObject food;
    private float stepRate, currentAngleZ;
    private Vector2 move;
    private List<Transform> tail = new List<Transform>();
    private bool isFoodEaten;
    private Vector2 lBorderPos, rBorderPos, uBorderPos, dBorderPos;
    
    private GameController gameController;
    private bool isGameOver, canTurn;
    private float turnTime;
    
    void Start()
    {
        currentAngleZ = 0.0f;
        stepRate = 0.3f;
        isFoodEaten = false;
        lBorderPos = GameObject.Find("WallLeft").transform.position;
        rBorderPos = GameObject.Find("WallRight").transform.position;
        uBorderPos = GameObject.Find("WallUp").transform.position;
        dBorderPos = GameObject.Find("WallDown").transform.position;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        turnTime = Time.time;
        isGameOver = false;
        InvokeRepeating("Movement", 0.1f, stepRate);
        SpawnFood();
    }
    void Update()
    {
        if (!food)
        {
            SpawnFood();
        }
        SnakeBehavior(90);
    }

    private void SnakeBehavior(float prevAngleZ)
    {
        SetDirection();
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, currentAngleZ);
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            move = Vector2.up;
        }

        if (prevAngleZ != currentAngleZ)
        {
            turnTime =  Time.time;
            canTurn = false;
        }
    }
    private void SpawnFood()
    {
        float x = (int)Random.Range(lBorderPos.x + 2f, rBorderPos.x - 2f);
        float y = (int)Random.Range(uBorderPos.x + 2f, dBorderPos.x - 2f);

        food = Instantiate(foodPref, new Vector3(x, y, 5f), Quaternion.identity);
    }
    void Movement()
    {
        Vector2 v = transform.position;
        transform.Translate(move);
        if (isFoodEaten)
        {
            GameObject g = Instantiate(tailPref, v, Quaternion.identity);
            g.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
            tail.Insert(0, g.transform);
            isFoodEaten = false;
        }
        else if(tail.Count > 0)
        {
            tail.Last().position = v;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
    void SetDirection()
    {
        if (canTurn)
        {
            if(Input.GetKeyDown(KeyCode.D) && currentAngleZ != 90.0f)
            {
                currentAngleZ = 270.0f;
            }if(Input.GetKeyDown(KeyCode.A) && currentAngleZ != 270.0f)
            {
                currentAngleZ = 90.0f;
            }if(Input.GetKeyDown(KeyCode.W) && currentAngleZ != 180.0f)
            {
                currentAngleZ = 0.0f;
            }if(Input.GetKeyDown(KeyCode.S) && currentAngleZ != 0.0f)
            {
                currentAngleZ = 180.0f;
            }
        }
    }

    private void Restart()
    {
        for (int i = 0; i < tail.Count; i++)
        {
            Destroy(tail[i].gameObject);
        }
        tail.Clear();
        transform.position = Vector2.zero;
        canTurn = true;
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag($"Food"))
        {
            Destroy(collision.gameObject);
            isFoodEaten = true;
            SpawnFood();
        }
       if(collision.gameObject.name == "WallR" && currentAngleZ != 270.0f)
        {
            transform.position = new Vector2(lBorderPos.x, transform.position.y);
        }
        else if (collision.gameObject.name == "WallL" && currentAngleZ != 90.0f)
        {
            transform.position = new Vector2(rBorderPos.x, transform.position.y);
        }
        else if (collision.gameObject.name == "WallU" && currentAngleZ != 0.0f)
        {
            transform.position = new Vector2(transform.position.x, uBorderPos.y);
        }
        else if (collision.gameObject.name == "WallD" && currentAngleZ != 180.0f)
        {
            transform.position = new Vector2(transform.position.x, dBorderPos.y);
        }
    }
}
