using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFishBullet : Bullet
{
    public static void Create(Transform pos, Vector3 spawnPos, Vector3 dir)
    {
        Bullet bullet = Instantiate(pos, spawnPos, Quaternion.identity).GetComponent<SpecialFishBullet>();
        bullet.SetDirection(dir);
    }
    // Update is called once per frame
    void Update()
    {
       Move(); 
    }

    //This is the basic movement for all bullets override with speical movement eventually
    public override void Move()
    {
        transform.position = (direction.normalized * speed * Time.deltaTime) + transform.position;

    }

    void OnTriggerEvent(Collider2D p)
    {
        if(p.CompareTag("Penguin") || p.CompareTag("BabyPenguin")){
            gameObject.SetActive(false);
        }
    }
}
