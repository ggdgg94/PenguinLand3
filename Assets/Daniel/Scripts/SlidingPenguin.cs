using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPenguin : Character 
{
    [SerializeField]
    public Transform player;
    Collider2D penguinBox;
    // Start is called before the first frame update
    void Start()
    {
        if(autoFill)
            SetUpLimits();
        player = FindObjectOfType<Player>().transform;
        SetDirection(); 
       //For now, we don't need this
       //animator = this.GetComponent<Animator>();
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
                gameObject.SetActive(false);
            break;
        }
    }

    void MovePenguin()
    {
        //SetMoveAnimation(direction);
        Move(direction);
        if(transform.position.x == minX)
            gameObject.SetActive(false);
        
        if(transform.position.x == maxX)
            gameObject.SetActive(false);

        if(transform.position.y == minY)
            gameObject.SetActive(false);

        if(transform.position.y == maxY)
            gameObject.SetActive(false);
    }

    public override void SetDirection()
    { 
        direction = (player.position - transform.position).normalized;
        direction.x = Mathf.RoundToInt(direction.x);
        direction.y = Mathf.RoundToInt(direction.y);
        transform.Rotate(0,0,Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);
    }
    public void SetDirection(Vector3 dir){ direction = dir;}
     void OnTriggerEnter2D(Collider2D p)
     {
        if(p.CompareTag("Bullet")){
            life -= 1;
        } 
     }


}
