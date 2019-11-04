using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Transform bullet; //set in Editor
    private Player player; //this gun should be attached to the player
    private Vector3 direction;
    void SetDirection(Vector3 dir){direction = dir;}
    Vector3 getDirection(){SetDirection(player.lastDirection); return direction;} 
    void Start()
    {
         player = this.GetComponentInParent<Player>();
    }

    public void Shoot() { SpecialFishBullet.Create(bullet,player.transform.position,getDirection()); }
}
