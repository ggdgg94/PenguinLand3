using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float xPositionLimit;
    public float yPositionLimit;
    public float speed;
    public Transform mSpawnPoint;
    public float minY = 1;
    public float maxY = 5;
    public float minX = 1;
    public float maxX = 5;
    
    public bool enteredVillage = false;
    public GameObject mInstance;
    private Transform target;
    
    
    void Awake()
    {
    }
        
    public void Setup()
    {
    }
    
    void Start()
    {
        speed = 3f;
        xPositionLimit = -7f;
        yPositionLimit = 0f;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
//        Move();
        //Declare variables for X and Y axis move directions, these range from -1 to 1.
        //These values allow us to choose between the cardinal directions: up, down, left and right.
        int xDir = 0;
        int yDir = 0;

        //If the difference in positions is approximately zero (Epsilon) do the following:
        if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)

            //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
            yDir = target.position.y > transform.position.y ? 1 : -1;

        //If the difference in positions is not approximately zero (Epsilon) do the following:
        else
            //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
            xDir = target.position.x > transform.position.x ? 1 : -1;

        Move();
    }

    void MoveTowardPlayer(int xDir, int yDir)
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x + xDir, position.y + yDir);
        transform.position = position;
    }

    void Move()
    {
        if (enteredVillage == false)
        {
            Vector2 position = transform.position;
            position = new Vector2(position.x - speed * Time.deltaTime, position.y);

            transform.position = position;

            if (transform.position.x < xPositionLimit)
            {
                enteredVillage = true;
                // Destroy(this);
            }
        }
    }
}
