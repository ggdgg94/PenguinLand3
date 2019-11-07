using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform item;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,10f,0);
    }
    void OnTriggerEvent(Collider2D p)
    {
        if(p.CompareTag("Player"))
            gameObject.SetActive(false);
    }
}
