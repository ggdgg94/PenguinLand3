using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPenguin : Character 
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
       animator = this.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case CharacterState.Normal:
                CheckLife();
                SetDirection();
                MovePenguin(); 
                break;
            case CharacterState.Defeated:
                gameObject.SetActive(false);
                break;
        }
    }

    public override void SetDirection()
    {
        direction = (player.position - transform.position).normalized;
        direction.x = Mathf.RoundToInt(direction.x);
        direction.y = Mathf.RoundToInt(direction.y);
    }

    public void MovePenguin()
    {
        SetMoveAnimation(direction);
        Move(direction);

    }

    /* Baby penguin on collision with player that isn't moving will
     * latch on to the player so idle animations will be used for 
     * the baby penguins */
}
