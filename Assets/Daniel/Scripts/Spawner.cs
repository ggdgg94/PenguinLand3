using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public bool active;
    public float spawnRate;
    public Vector3 spawnPoint;
    public int numSpawns;

    public bool randomX = true;
    public bool randomY = true;
    
    public virtual void Spawn(){}
    public virtual void Spawn(Character p){}
    public virtual void Spawn(Item b){}

}
