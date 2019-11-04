using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Transform bullet; //set in Editor
    private Player player; //this gun should be attached to the player
    private Vector3 direction;
    private Vector3 m = new Vector3(0, 0.4f, 0);
    void SetDirection(Vector3 dir){direction = dir;}
    Vector3 getDirection(){SetDirection(player.lastDirection); return direction;} 
    void Start()
    {
         player = this.GetComponentInParent<Player>();
    }

    //Testing the shots of the gun, in the near future shoot different amounts depending on bullet type
    public void Shoot() 
    { 
        SpecialFishBullet.Create(bullet,player.transform.position + m,getDirection());
        SpecialFishBullet.Create(bullet,player.transform.position + m * 2,getDirection());
        SpecialFishBullet.Create(bullet,player.transform.position,getDirection());
    }
}
