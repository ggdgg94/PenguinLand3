using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour{
    public float minY, maxY;
    public float posX;
    public float timer = 1f;
    private Vector3 position;
    public GameObject penguinPrefab;
    Camera p;
    void Start(){
        p = FindObjectOfType<Camera>();
        minY = -(p.scaledPixelHeight / 10);
        maxY = p.scaledPixelHeight / 10;
        position = transform.position;
        Invoke("SpawnPenguins", timer);

    }

    void Update(){

    }

    void SpawnPenguins(){
        position.y = Random.Range(minY, maxY);

        Instantiate(penguinPrefab, position, Quaternion.Euler(0f,0f,0f));
        Invoke("SpawnPenguins", timer);
    }
}
