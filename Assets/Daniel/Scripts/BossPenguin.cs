using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPenguin : Character
{
    public Transform player;
    public Vector3 distance;
    // Start is called before the first frame update
    void Start()
    {
        if(autoFill)
            SetUpLimits();
       animator = this.GetComponent<Animator>(); 
       player = FindObjectOfType<Player>().transform;
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
            case CharacterState.Bouncing:
                CheckLife();
                Fly();
                break;
            case CharacterState.Defeated:
                gameObject.SetActive(false);
                Destroy(this.gameObject, 3f);
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
        Move(direction);
    }
    public void Fly()
    {
        state = CharacterState.Bouncing;
        dashSpeed -= dashSpeed * 2f * Time.deltaTime;
        Move(-direction, dashSpeed);
        if(dashSpeed < 1f){
            state = CharacterState.Normal;
            dashSpeed = 5f;
            transform.rotation = Quaternion.identity;
        }
    }

    /* Baby penguin on collision with player that isn't moving will
     * latch on to the player so idle animations will be used for 
     * the baby penguins 
     *
     * Things we can collide with so far Player and bullets
     *
     * Todo add in collisions with other penguins
     * Look into using a ray cast instead
     */

     void OnTriggerEnter2D(Collider2D p)
     {
        if(p.CompareTag("Player")){
            dashSpeed = 5f; //prepare to be flung off
            Fly();
        } 
        if(p.CompareTag("Bullet")){
            life -= 1;
            Fly();
        }
     }
}
