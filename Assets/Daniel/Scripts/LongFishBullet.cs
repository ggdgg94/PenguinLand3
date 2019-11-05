using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongFishBullet : Bullet 
{
    public static void Create(Transform pos, Vector3 spawnPos, Vector3 dir)
    {
        Bullet bullet = Instantiate(pos, spawnPos, Quaternion.identity).GetComponent<LongFishBullet>();
        bullet.SetDirection(dir);
    }
    void SetRender()
    {
        if(direction.x < 0)
            r.flipY = true;
    }
    void Update()
    {
        Move(); 
    }

    //This is the basic movement for all bullets override with speical movement eventually
    public override void Move()
    {
        SetRender();
        transform.position = (direction.normalized * speed * Time.deltaTime) + transform.position;
    }

}
