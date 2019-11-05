using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFishBullet : Bullet 
{
    [SerializeField]
    int feed = 3;
    public static void Create(Transform pos, Vector3 spawnPos, Vector3 dir)
    {
        Bullet bullet = Instantiate(pos, spawnPos, Quaternion.identity).GetComponent<BigFishBullet>();
        bullet.SetDirection(dir);
    }
    void SetRender()
    {
        if(direction.x < 0)
            r.flipY = true;
    }
    void Update()
    {
        CheckFoodLeft();
        Move(); 
    }
    void CheckFoodLeft()
    {
        if(feed <= 0)
            gameObject.SetActive(false);

    }

    //This is the basic movement for all bullets override with speical movement eventually
    public override void Move()
    {
        SetRender();
        transform.position = (direction.normalized * speed * Time.deltaTime) + transform.position;

    }

    void OnTriggerEvent(Collider2D p)
    {
        if(p.CompareTag("Penguin") || p.CompareTag("BabyPenguin")){
            --feed;
        }

        //Might change this later so that it cannot feed a BigPenguin unless it has 3 feed
        if(p.CompareTag("BigPenguin")){
            feed = 0;
        }
    }
}
