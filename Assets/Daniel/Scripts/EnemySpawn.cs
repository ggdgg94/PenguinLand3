using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour{
    public float minY, maxY;
    public float posX;
    public float timer = 1f;
    private Vector3 position;
    public GameObject penguinPrefab;
    void Start(){
        position = new Vector3(posX, 0f, 0f);
        Invoke("SpawnPenguins", timer);
        Debug.Log("Spawn at " + position.y);

    }

    void Update(){

    }

    void SpawnPenguins(){
        position.y = Random.Range(minY, maxY);

        Instantiate(penguinPrefab, position, Quaternion.Euler(0f,0f,0f));
        Invoke("SpawnPenguins", timer);
    }
}
