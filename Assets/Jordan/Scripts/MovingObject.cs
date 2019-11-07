using UnityEngine;
using System.Collections;

//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
//    public LayerMask blockingLayer;         //Layer on which collision will be checked.
    
    
    private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
    private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
    private float inverseMoveTime;          //Used to make movement more efficient.
    
    
    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Awake ()
    {
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent <BoxCollider2D> ();
        
        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent <Rigidbody2D> ();
        
        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 1f / moveTime;
    }
}
