using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPenguin : Character
{
    private Character p;
    // Start is called before the first frame update
    void Start()
    {
        if(autoFill)
            SetUpLimits();
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
            case CharacterState.Eating:
                Invincible();
                break;
            case CharacterState.Defeated:
                gameObject.SetActive(false);
                Destroy(this.gameObject);
                break;
        }
        
    }
    void MovePenguin()
    {
        SetMoveAnimation(direction);
        Move(direction);
        if(transform.position.x == minX || transform.position.x == maxX){
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        if(transform.position.y == minY || transform.position.y == maxY){
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        
    }

    public void SetDirection()
    {
        direction = new Vector3(0,-1,0);
    }

    void OnTriggerEnter2d(Collider2D p)
    {
        if(p.CompareTag("Bullet")){
            if(p.name.Equals("BigFishBullet")){
                life = 0;
            }else{
                life -= 1;
            }
        }
    }
}
