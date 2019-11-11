using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public enum PenguinType{
        Regular,
        Baby,
        Sliding,
        Fat
    };
    public enum BulletType{
        Regular,
        Special,
        Pierce,
        Big
    }
    public bool active;
    public float spawnRate;
    public Vector3 spawnPoint;
    public int numSpawns;
    
    public virtual void Spawn(Character p){}
    public virtual void Spawn(Item b){}

}
