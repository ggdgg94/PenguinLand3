using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFishBullet : MonoBehaviour
{
    public float speed = 100;
    public float deactivateTimer = 3f;
    Vector3 direction;
    Vector3 tmp;
    public static void Create(Transform pos, Vector3 spawnPos, Vector3 dir)
    {
        SpecialFishBullet bullet = Instantiate(pos, spawnPos, Quaternion.identity).GetComponent<SpecialFishBullet>();
        bullet .SetDirection(dir);
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(direction);
        Invoke("DeactivateGameObject", deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {
       Move(); 
    }
    public void SetDirection(Vector3 dir){direction = dir;}
    void Move()
    {
        tmp = (direction.normalized * speed * Time.deltaTime) + transform.position;
        transform.position = tmp;

    }
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
