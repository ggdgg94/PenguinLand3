using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
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

    public enum CharacterState{ Normal, Dashing, Damaged, Defeated};


    public CharacterState state;
    public int life;

    //Movement related variables
    public float minX, maxX, minY, maxY;
    public Vector3 direction = new Vector3();
    public Vector3 lastDirection;
    public Vector3 tmpPosition;
    public Animator animator;


    // Start is called before the first frame update
    public void CheckLife()
    {
        if(life <= 0)
            state = CharacterState.Defeated;
    }
    public virtual void SetDirection(){}
    public virtual void SetAnimation(Vector3 dir){}
    public virtual void Move(Vector3 dir, float spe)
    {
            lastDirection = dir;
            tmpPosition = (dir.normalized * spe * Time.deltaTime) + transform.position;
            tmpPosition.x = Mathf.Clamp(tmpPosition.x, minX, maxX);
            tmpPosition.y = Mathf.Clamp(tmpPosition.y, minY, maxY);
            transform.position = tmpPosition;
    }
}
