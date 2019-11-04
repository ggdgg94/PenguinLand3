using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Transform bullet; //set in Editor
    private Player player; //this gun should be attached to the player
    private Vector3 direction;
    SpriteRenderer rend; 
    private Vector3 m = new Vector3(0, 0.4f, 0);
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        if(direction.x < 0){
            rend.flipX = true;
            transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
        }else{
            rend.flipX = false;
            transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
    }
    Vector3 getDirection(){SetDirection(player.lastDirection); return direction;} 
    void Start()
    {
         player = this.GetComponentInParent<Player>();
         rend = this.GetComponent<SpriteRenderer>();
    }

    //Testing the shots of the gun, in the near future shoot different amounts depending on bullet type
    public void Shoot() 
    { 
        SpecialFishBullet.Create(bullet,player.transform.position + m,getDirection());
        SpecialFishBullet.Create(bullet,player.transform.position + m * 2,getDirection());
        SpecialFishBullet.Create(bullet,player.transform.position,getDirection());
    }
}
