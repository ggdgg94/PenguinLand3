using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character 
{
    //Related to shooting fish, all can be set in Editor
    [SerializeField]
    private float shootTimer;
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private Gun gun; //Set in Editor, should be attached to Player prefab
    [SerializeField]


    //Related to Dashing?
    Collider2D playerCollider;
    public ArrayList contacts;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        playerCollider = this.GetComponent<BoxCollider2D>();
        contacts = new ArrayList();
        state = CharacterState.Normal;
        if(autoFill)
            SetUpLimits();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case CharacterState.Normal:
                CheckLife();
                SetDirection();
                MovePlayer(); 
                CheckDash();
                Shoot();
                break;
            case CharacterState.Dashing:
                Dash();
                break;
            case CharacterState.Damaged:
                CheckLife();
                Invincible();
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
            gun.SetDirection(direction);
            Move(direction);
        }
    }

    void Shoot()
    {
        if(shootTimer < cooldownTime){
            shootTimer += Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.K)){
            if(shootTimer >= cooldownTime){
                shootTimer = 0;
                gun.Shoot(); 
             }
        }
    }

    void CheckDash()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            state = CharacterState.Dashing;
            dashSpeed = 50f;
        }
    }
    void Dash()
    {
        Move(lastDirection, dashSpeed);
        dashSpeed -= dashSpeed * 8f * Time.deltaTime;

        //There is probably a better way to do this
        /* 
        if(contacts.Count != 0){
            foreach(Collider2D a in contacts){
                a.GetComponentInParent<BabyPenguin>().Fly();
            }
            contacts.Clear();
            moveSpeed = 5;
        }
        */
        moveSpeed = 5;
        
        if(dashSpeed < 1f )
            state = CharacterState.Normal;
    }

    void OnTriggerEnter2D(Collider2D p)
    {
        if(p.CompareTag("BabyPenguin")){
            if(state == CharacterState.Normal){
                moveSpeed = Mathf.Clamp(moveSpeed-1, 0, 5);
                //contacts.Add(p);
            }
            if(state == CharacterState.Dashing){
                p.GetComponentInParent<BabyPenguin>().Crash(dashSpeed);
            }

        }

        //Baby and Big should be the only exceptions to Penguin so the rest act regular
        if(p.CompareTag("Penguin") && state == CharacterState.Normal){
            life -= 1;
            state = CharacterState.Damaged;
        }

        if(p.CompareTag("BigPenguin")){
            if(state == CharacterState.Normal){
                life -= 2;
                state = CharacterState.Damaged;
            }
            if(state == CharacterState.Dashing){
                //Bounce here
            }
        }

        if(p.CompareTag("BItem")){
            gun.SetBullet(p.GetComponentInParent<Item>().item);
        }
    }

}
