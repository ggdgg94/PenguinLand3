using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularPenguin : Character
{
    // Start is called before the first frame update
    void Start()
    {
        if(autoFill)
            SetUpLimits(20);
        SetDirection();        
        animator = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case CharacterState.Normal:
            CheckLife();
            MovePenguin();
            break;
            case CharacterState.Defeated:
            SimpleGameManager.score += 1;
            Die();
            SoundManager.PlaySound("GetHit1");
            break;
        }
        
    }
    void MovePenguin()
    {
        SetMoveAnimation(direction);
        Move(direction);
        if(transform.position.x == minX){
            Die();
        }
    }
    public override void SetDirection()
    {
        direction = new Vector3(-1f, 0, 0);
    }

    public void SetDirection(Vector3 dir){ direction = dir; }


     void OnTriggerEnter2D(Collider2D p)
    {
        if(p.CompareTag("Bullet")){
            life -= 1;
            SoundManager.PlaySound("GetHit0");

        }
    }
}
