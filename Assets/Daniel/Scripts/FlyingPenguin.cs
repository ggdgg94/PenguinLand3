using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPenguin : Character 
{
    public Transform player;
    public Transform shadow;
    public Character [] drops; //list of penguins to drop
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        if(autoFill)
            SetUpLimits();
       player = FindObjectOfType<Player>().transform;
       timer = Random.Range(0, 10f);
    }
    // Update is called once per frame
    void Update()
    {
        SetDirection();
        MovePenguin(); 
    }
    public override void SetDirection()
    {
        //follow player in x direction
        direction = (player.position - transform.position).normalized;
        direction.x = Mathf.RoundToInt(direction.x);
        //direction.y = Mathf.RoundToInt(direction.y);
    }
    public void MovePenguin()
    {
        Move(direction);
    }
    public void Drop()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            int r = Random.Range(0, drops.Length);
            //need to play a sound here to indicate a drop
            Instantiate(drops[r], shadow.position, Quaternion.identity);
            Die();
        }

    }
}
