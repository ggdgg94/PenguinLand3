using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSpawner : Spawner
{
    Character[] spawns;
    Vector3 tmpPoint;
    int qPosition = 0;
    public enum SpawnType{
        Random,
        RandomX,
        RandomY,
        Arrow,
        Pair,
        PairX,
        PairY
    };
    public PenguinType type;
    // Start is called before the first frame update
    void Start()
    {
        tmpPoint = spawnPoint;
        if(numSpawns > 0)
            spawns = new Character[numSpawns];
        
    }
    public override void Spawn(Character p)
    {
        if(spawns != null){
            if(qPosition < numSpawns){

            }

        }else{
            SpawnUnlimitedRandom(p);
        }

    }
    void SpawnUnlimitedRandom(Character p)
    {
        tmpPoint.x = Random.Range(p.minX, p.maxX);
        tmpPoint.y = Random.Range(p.minY, p.maxY);
        Instantiate(p, tmpPoint, Quaternion.identity);

    }
    void SpawnRandom(Character p, Vector3 s)
    {
        tmpPoint.x = Random.Range(p.minX, p.maxX);
        tmpPoint.y = Random.Range(p.minY, p.maxY);       
        p.transform.position = tmpPoint;
        p.gameObject.SetActive(true);

    }
    void SpawnArrow(Character p, Vector3 s, int i)
    {
        tmpPoint.x = Random.Range(p.minX, p.maxX);
        tmpPoint.y = Random.Range(p.minY, p.maxY);
        spawns[i].transform.position = tmpPoint;

        //This number needs to scale 
        tmpPoint.x += 1;
        tmpPoint.y += 1;
        spawns[i+1].transform.position = tmpPoint;
        tmpPoint.y -= 2;
        spawns[i+2].transform.position = tmpPoint;

        spawns[i].gameObject.SetActive(true);
        spawns[i+1].gameObject.SetActive(true);
        spawns[i+2].gameObject.SetActive(true);

    }
    void SpawnPair(Character p, Vector3 s)
    {

    }
    void SpawnPairX(Character p, Vector3 s)
    {

    }
    void SpawnPairY(Character p, Vector3 s)
    {

    }
    // Update is called once per frame
}
