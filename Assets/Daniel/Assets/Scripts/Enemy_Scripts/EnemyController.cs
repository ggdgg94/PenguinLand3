using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static readonly string[,] movement = new string[,] {
        {"MoveSW", "MoveW", "MoveNW"},
        {"MoveS", "MoveS", "MoveN"},
        {"MoveSE", "MoveE", "MoveNE"},

    };

    private enum CharacterState{ Normal, Dashing, Damaged, Defeated};
    private CharacterState playerState;
    
    public float speed = 3f;
    public float maxX, minX;
    private Vector3 tmpPos;
    private Vector3 direction;

    private Animator animator;

    public float life = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        playerState = CharacterState.Normal;
        direction = new Vector3(-1f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerState){
            case CharacterState.Normal:
                CheckLife(); //Make sure we are alive
                MovePlayer(); // nothing of note
            break;
            case CharacterState.Defeated:
            break;
        }

    }

    void CheckLife(){
        if(life <= 0f){
            playerState = CharacterState.Defeated;
        }

    }


    void GetDirection(){
        direction.x = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        direction.y = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
    }
    void MovePlayer(){
        animator.Play(movement[(int)direction.x + 1, (int)direction.y + 1]);
        Move(direction, speed);

    }

    void Move(Vector3 d, float s){
            tmpPos = (d.normalized * s * Time.deltaTime) + transform.position;

            //Check if new Position is valid
            //tmpPos.x = Mathf.Clamp(tmpPos.x, minX, maxX);
            if(tmpPos.x < minX){
                gameObject.SetActive(false);
            }

            transform.position = tmpPos;
    }
    
    void OnTriggerEnter2D(Collider2D p){
        if(p.CompareTag("Bullet")){
            life -= 1f;
            ScoreScript.scoreValue += 1;
            gameObject.SetActive(false);
        }
    }
}
