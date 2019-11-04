using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 25f;
    public float deactivateTimer = 3f;
    public Vector3 direction;
    void Start()
    {
        Invoke("DeactivateGameObject", deactivateTimer);    
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        transform.Rotate(0,0,Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    } 
    public virtual void Move(){}
    public void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

}
