using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedCount : MonoBehaviour
{

    public EnemyController feedcount;
    public int Fcount = 0;


    void Start()
    {
        feedcount = GameObject.FindGameObjectWithTag("Penguin(Clone)").GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        Fcount = Fcount + feedcount.count;
    }
}
