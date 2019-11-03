using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character 
{
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        state = CharacterState.Normal;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case CharacterState.Normal:
                CheckLife();
                SetDirection();
                MovePlayer(); 
                break;
            case CharacterState.Defeated:
                gameObject.SetActive(false);
                break;
        }

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
