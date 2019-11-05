using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private static readonly string[,] idle = new string[,] {
        {"IdleSW", "IdleW", "IdleNW"},
        {"IdleS", "IdleS", "IdleN"},
        {"IdleSE", "IdleE", "IdleNE"},

    };

    private enum CharacterState{ Normal, Dashing, Damaged, Defeated};
    private CharacterState playerState;
    public GameObject LoseUI;
    public float speed = 5f;
    public float dashSpeed = 20f;
    public float maxX, maxY, minX, minY;
    private Vector3 tmpPos;
    private Vector3 direction = new Vector3(); 
    public float shootTimer = 0.35f;
    private float currentTimer;
    private bool canShoot;

    //private Rigidbody2D rb;

    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject fishShot;

    [SerializeField]
    private Transform shootPoint;

    private SpriteRenderer renderer;
    private Animator animator;
    private string lastDirection = "IdleS";
    [SerializeField]
    private float invincibleTimer = 1.0f;
    public int life = 5;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = shootTimer;

        //rb = this.GetComponent<Rigidbody2D>();
        renderer = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        playerState = CharacterState.Normal;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerState){
            case CharacterState.Normal:
                CheckLife(); //Make sure we are alive
                Bomb(); //pauses game so the rest of the commands should still run smoothly afterward
                MovePlayer(); // nothing of note
                CheckDash(); // while dashing can't do other options but afterward it should be fine
                Override(); //check to see if we put the gun into override mode first
                Shoot(); //shoot based on certain conditions(check we can shoot, overdirve on/off etc)
            break;
            case CharacterState.Dashing:
                Dash();
            break;
            case CharacterState.Damaged:
                Invincible();
                MovePlayer();
            break;
            case CharacterState.Defeated:
                animator.Play("Invincible");
                gameObject.SetActive(false);
            break;
        }

    }
    void Invincible(){
        invincibleTimer -= Time.deltaTime;
        if(invincibleTimer < 0f){
            invincibleTimer = 1.0f;
            playerState = CharacterState.Normal;
            renderer.color = new Color(254f, 254f, 254f, 255f);

        }

    }
    void CheckLife(){
        if(life <= 0f){
            playerState = CharacterState.Defeated;
        }

    }
    void Bomb(){
        //Trigger the bomb to clear the screen of penguins

    }

    //testing basic dash function
    void CheckDash(){
        if(Input.GetKeyDown(KeyCode.J)){
            playerState = CharacterState.Dashing;
            GetDirection();
            dashSpeed = 50f;
        }
    }
    void Dash(){
        Move(direction, dashSpeed);
        dashSpeed -= dashSpeed * 7f * Time.deltaTime;
        if(dashSpeed < 5f)
            playerState = CharacterState.Normal;

    }

    void GetDirection(){
        direction.x = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        direction.y = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
    }
    void MovePlayer(){
        GetDirection();

        //If no direction pressed, play last direction animation
        if(direction.x == 0 && direction.y == 0){
            animator.Play(lastDirection);
        }else{
            animator.Play(idle[(int)direction.x+1,(int)direction.y+1]);
            lastDirection = idle[(int)direction.x+1,(int)direction.y+1];
            if(direction.y > 0f)
                gun.GetComponent<SpriteRenderer>().sortingOrder = 1;
            else
                gun.GetComponent<SpriteRenderer>().sortingOrder = 3;
            //gun.GetComponent<Transform>().rotation.z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //SetRotation(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            Move(direction, speed);
        }

    }

    void Move(Vector3 d, float s){
            tmpPos = (d.normalized * s * Time.deltaTime) + transform.position;

            //Check if new Position is valid
            tmpPos.x = Mathf.Clamp(tmpPos.x, minX, maxX);
            tmpPos.y = Mathf.Clamp(tmpPos.y, minY, maxY);

            transform.position = tmpPos;
    }
    
    //Control bullet amount
    void Shoot(){
        shootTimer += Time.deltaTime;
        if(shootTimer > currentTimer){
            canShoot = true;
        }

        if(Input.GetKeyDown(KeyCode.K)){
            if(canShoot){
                canShoot = false;
                shootTimer = 0f;                               
                Instantiate(fishShot, shootPoint.position, Quaternion.identity);
            }
            
        }
    }

    void Override(){
        if(Input.GetKeyDown(KeyCode.J)){
            currentTimer = 0.15f;
        }
    }


    void OnTriggerEnter2D(Collider2D p){
        if(p.CompareTag("Penguin") && playerState == CharacterState.Normal){
            life = life-1;
            playerState = CharacterState.Damaged;
            renderer.color = new Color(255f, 0f, 0f, 50);
            if (life == 0)
            {
                LoseUI.SetActive(true);
            }
        }
    }
}
