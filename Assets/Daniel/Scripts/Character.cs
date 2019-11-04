using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    //Related to Animations    
    public static readonly string[,] idle = new string[,] {
        {"IdleSW", "IdleW", "IdleNW"},
        {"IdleS", "IdleS", "IdleN"},
        {"IdleSE", "IdleE", "IdleNE"},

    };

    public static readonly string[,] move = new string[,] {
        {"MoveSW", "MoveW", "MoveNW"},
        {"MoveS", "MoveS", "MoveN"},
        {"MoveSE", "MoveE", "MoveNE"},

    };

    public enum CharacterState { Normal, Dashing, Damaged, Defeated };
    public CharacterState state;

    //Related to Life
    public int life; //to be set in the Editor
    public float invincilbeTimer; //set in Editor

    //Related to Movement
    public float moveSpeed;
    public float dashSpeed;
    public Vector3 direction;
    public Vector3 lastDirection;
    
    //Related to Location
    public float maxX, minX, maxY, minY; //To be set in Editor
    public Vector3 tmpPosition;

    //Related to Animations
    public Animator animator; 

    public virtual void SetDirection(){}
    public void Move(Vector3 dir)
    {
        lastDirection = dir;
        tmpPosition = (dir.normalized * moveSpeed * Time.deltaTime) + transform.position;
        tmpPosition.x = Mathf.Clamp(tmpPosition.x, minX, maxX);
        tmpPosition.y = Mathf.Clamp(tmpPosition.y, minY, maxY);
        transform.position = tmpPosition;
    }
    public void Move(Vector3 dir, float spe)
    {
        lastDirection = dir;
        tmpPosition = (dir.normalized * spe * Time.deltaTime) + transform.position;
        tmpPosition.x = Mathf.Clamp(tmpPosition.x, minX, maxX);
        tmpPosition.y = Mathf.Clamp(tmpPosition.y, minY, maxY);
        transform.position = tmpPosition;
    }
    public void SetIdleAnimation(Vector3 dir){animator.Play(idle[(int)dir.x + 1, (int)dir.y + 1]);}
    public void SetMoveAnimation(Vector3 dir){animator.Play(move[(int)dir.x + 1, (int)dir.y + 1]);}
    
    public void CheckLife() 
    {
        if(life <= 0)
          state = CharacterState.Defeated;
    }

    public void Invincible()
    {
        invincilbeTimer -= Time.deltaTime;
        if(invincilbeTimer < 0f){
            invincilbeTimer = 1.0f;
            state = CharacterState.Normal;
        }       
    }
}