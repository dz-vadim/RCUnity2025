using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPref, tailPrefab; // storing prefabs
    private GameObject food; // storing the current spawned food
    private float stepRate, currentAngleZ; // snake's step, head angle
    // vector for where we move and where we can move next
    private Vector2 move;
    private List<Transform> tail = new List<Transform>(); // storing tail pieces
    private bool isFoodEaten; // if the food was eaten
    // vectors for the wall positions
    private Vector2 lBoarderPos, rBoarderPos, uBoarderPos, dBoarderPos;
    private GameController controller;
    // tells if the game is over and if we can turn at the moment
    private bool isGameOver, canTurne;
    private float turnTime; // time of the turn

    void Start()
    {
        currentAngleZ = 0.0f;
        stepRate = 0.3f;
        isFoodEaten = false;
        lBoarderPos = GameObject.Find("WallL").transform.position;
        rBoarderPos = GameObject.Find("WallR").transform.position;
        uBoarderPos = GameObject.Find("WallU").transform.position;
        dBoarderPos = GameObject.Find("WallD").transform.position;
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        turnTime = Time.time; // record the time since the game started
        isGameOver = false;   // will change to true when we lose
        InvokeRepeating("Movement", 0.1f, stepRate);
        SpawnFood();
        switch (MenuController.colorNum)
        { // set this in Start()
            case 1: // white
                GetComponent<Renderer>().material.color = Color.white;
                break;
            case 2: // yellow
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case 3: // green
                GetComponent<Renderer>().material.color = new Color(0.0f, 1f, 0.52f);
                break;
            default:
                GetComponent<Renderer>().material.color = Color.white;
                break;
        }
    }

    void Update()
    {
        SnakeBehaviour(currentAngleZ);
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            move = Vector2.up;
            controller.startText.color = Color.clear;
        }
        if (food == null) // do this in Update()
        {
            SpawnFood();
        }
    }

    void SnakeBehaviour(float prevAngleZ)
    {
        SetDirection();
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, currentAngleZ);
        if (Time.time > turnTime + 0.2f) // if 0.2 seconds have passed
        {
            canTurne = true; // we can turn
        }
        if (prevAngleZ != currentAngleZ) // if our turning angle has changed
        {                                    // from the last move
            turnTime = Time.time; // if yes, record the time of the turn
            canTurne = false;    // we cannot turn
        }
    }

    void SpawnFood()
    {
        float x = (int)Random.Range(lBoarderPos.x + 2f, rBoarderPos.x - 2f);
        float y = (int)Random.Range(dBoarderPos.y + 2f, uBoarderPos.y - 2f);
        food = Instantiate(foodPref, new Vector3(x, y, 5f), Quaternion.identity);
    }

    void Movement()
    {
        Vector2 v = transform.position; // record the current snake position
        transform.Translate(move); // move the head in the given direction
        if (isFoodEaten) // if the food was eaten, create a new tail piece at the previous head position
        {
            GameObject g = Instantiate(tailPrefab, v, Quaternion.identity);
            g.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
            tail.Insert(0, g.transform);
            isFoodEaten = false;
        }
        else if (tail.Count > 0) // otherwise, move the last tail piece to the previous head position
        {
            tail.Last().position = v;       // don't forget to include using System.Linq
            tail.Insert(0, tail.Last());    // to use the Last() method
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void SetDirection()
    {
        if (canTurne) // check if we can turn
        {
            if (Input.GetKeyDown(KeyCode.D) && currentAngleZ != 90.0f)
            {
                currentAngleZ = 270.0f;
            }
            else if (Input.GetKeyDown(KeyCode.A) && currentAngleZ != 270.0f)
            {
                currentAngleZ = 90.0f;
            }
            else if (Input.GetKeyDown(KeyCode.W) && currentAngleZ != 180.0f)
            {
                currentAngleZ = 0.0f;
            }
            else if (Input.GetKeyDown(KeyCode.S) && currentAngleZ != 0.0f)
            {
                currentAngleZ = 180.0f;
            }
        }
    }

    private void Restart()
    {
        for (int i = 0; i < tail.Count; i++)
        { // go through all tail elements
            Destroy(tail[i].gameObject); // destroy each one
        }
        tail.Clear(); // clear the list itself
        // move the snake to the starting coordinates
        transform.position = new Vector2(0f, 0f);
        controller.score = 0; // reset the score
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Food") // check if we collided with the food
        {
            Destroy(col.gameObject); // destroy the eaten food
            isFoodEaten = true;
            controller.score++;
            SpawnFood(); // create a new one
        }
        if (col.gameObject.CompareTag("Wall"))
        { // if we hit any wall (don't forget to add the tag to the walls)
            turnTime = Time.time; // record the time of the collision
            canTurne = false; // make turning impossible
        }
        if (col.gameObject.CompareTag("Snake"))
        { // if we hit the tail (don't forget to add the tag to the prefab)
            Restart(); // restart the game
        }

        // teleport the snake from one wall to the opposite
        if (col.gameObject.name == "WallR" && currentAngleZ == 270.0f)
        {
            transform.position = new Vector2(lBoarderPos.x, transform.position.y);
        }
        else if (col.gameObject.name == "WallL" && currentAngleZ == 90.0f)
        {
            transform.position = new Vector2(rBoarderPos.x, transform.position.y);
        }
        else if (col.gameObject.name == "WallU" && currentAngleZ == 0.0f)
        {
            transform.position = new Vector2(transform.position.x, dBoarderPos.y);
        }
        else if (col.gameObject.name == "WallD" && currentAngleZ == 180.0f)
        {
            transform.position = new Vector2(transform.position.x, uBoarderPos.y);
        }
    }
}
