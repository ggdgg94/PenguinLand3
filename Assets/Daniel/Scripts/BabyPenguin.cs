using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPenguin : Character 
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
            case CharacterState.Latching:
                CheckLife();
                Latch();
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
        SetMoveAnimation(direction);
        Move(direction);
    }
    public void Latch()
    { 
        transform.position = player.position - distance; 
        if(player.GetComponent<Player>().state == CharacterState.Dashing)
            Fly();
    }
    public void Fly()
    {
        state = CharacterState.Bouncing;
        dashSpeed -= dashSpeed * 2f * Time.deltaTime;
        Move(-direction, dashSpeed);
        transform.Rotate(0,0,Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        if(dashSpeed < 1f){
            state = CharacterState.Normal;
            dashSpeed = 30f;
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
        if(p.CompareTag("Player") && player.GetComponent<Player>().state == CharacterState.Normal){
            distance = (player.position - transform.position) / 2;
            state = CharacterState.Latching;
            dashSpeed = 30f; //prepare to be flung off
        } 
        if(p.CompareTag("Bullet") && state != CharacterState.Latching){
            life -= 1;
        }
     }
}
