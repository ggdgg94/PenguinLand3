using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPenguin : Character 
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
       animator = this.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
       SetDirection(); 
       MovePenguin();
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
}
