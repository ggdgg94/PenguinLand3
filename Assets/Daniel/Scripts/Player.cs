using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character 
{
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
       SetDirection();
       MovePlayer(); 
    }

    public override void SetDirection(){direction.Set(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")),Mathf.RoundToInt(Input.GetAxisRaw("Vertical")),0);}
    public void MovePlayer()
    {
        if(direction.x == 0 && direction.y == 0)
            SetIdleAnimation(lastDirection);
        else{
            SetIdleAnimation(direction);
            Move(direction);
        }
    }
}
