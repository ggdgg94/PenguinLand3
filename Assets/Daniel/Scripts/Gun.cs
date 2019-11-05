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
    [SerializeField]
    Sprite[] sprites = new Sprite[4]; //Set in Editor
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

         
        if(bullet.name.Equals("LongFishBullet"))
            transform.position = player.transform.position + (direction);
        if(bullet.name.Equals("SpecialFishBullet"))
            transform.position = player.transform.position + (0.3f * direction);       
        
    }
    Vector3 getDirection(){SetDirection(player.lastDirection); return direction;} 
    void Start()
    {
         player = this.GetComponentInParent<Player>();
         rend = this.GetComponent<SpriteRenderer>();
         SetBullet(bullet);
    }

    public void SetBullet(Transform b)
    { 
        bullet = b; 
        if(bullet.name.Equals("SpecialFishBullet")){
            rend.sprite = sprites[3];
            transform.position = player.transform.position + (direction);
        }else if(bullet.name.Equals("LongFishBullet")){
            rend.sprite = sprites[2];
            transform.position = player.transform.position;
        }else if(bullet.name.Equals("BigFishBullet")){
            rend.sprite = sprites[1];
            transform.position = player.transform.position;
        }else{
            rend.sprite = sprites[0];
            transform.position = player.transform.position;
        }
    }

    //Testing the shots of the gun, in the near future shoot different amounts depending on bullet type
    public void Shoot()
    { 
        getDirection();
        if(bullet.name.Equals("SpecialFishBullet")){
            SpecialFishBullet.Create(bullet,player.transform.position + m,direction);
            SpecialFishBullet.Create(bullet,player.transform.position + m * 2,direction);
            SpecialFishBullet.Create(bullet,player.transform.position,direction);
        }else if(bullet.name.Equals("LongFishBullet")){
            LongFishBullet.Create(bullet, player.transform.position + direction, direction);
        }else if(bullet.name.Equals("BigFishBullet")){
            BigFishBullet.Create(bullet, player.transform.position + direction, direction);
        }else{
            RegularFishBullet.Create(bullet, player.transform.position + direction, direction);

        }
    }
}
