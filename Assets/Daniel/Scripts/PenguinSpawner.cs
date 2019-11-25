using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSpawner : Spawner
{
    public Character penguinPrefab;
    Character[] spawns;
    Vector3 tmpPoint;
    int qPosition = 0;
    public enum SpawnType{
        None,
        Random,
        Arrow,
        Pair,
    };
    public SpawnType stype;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        tmpPoint = spawnPoint;
        if(numSpawns > 0){
            spawns = new Character[numSpawns];
            for(int i = 0; i < numSpawns; ++i){
                spawns[i] = Instantiate(penguinPrefab, spawnPoint, Quaternion.identity);
                spawns[i].gameObject.SetActive(false);
            }
        }
        currentTime = spawnRate;
    }
    public override void Spawn()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = spawnRate;
            if (qPosition < numSpawns)
            {
                tmpPoint = spawnPoint;
                if(randomX)
                    tmpPoint.x = Random.Range(penguinPrefab.minX, penguinPrefab.maxX);
                if(randomY)
                    tmpPoint.y = Random.Range(penguinPrefab.minY, penguinPrefab.maxY);
                switch (stype)
                {
                    case SpawnType.None:
                    case SpawnType.Random:
                        SpawnRandom();
                        break;
                    case SpawnType.Arrow:
                        SpawnArrow();
                        break;
                    case SpawnType.Pair:
                        SpawnPair();
                        break;
                }
            }else{
                active = false;
            }

        }

    }

    //We use class globals(?) on all of these probaby not the best practice
    //A more practiced approach would be
    //void SpawnRandom(Character[] penguins, int i), feel free to change if you want

    void SpawnRandom()
    {
        if(spawns != null){      
        spawns[qPosition].transform.position = tmpPoint;
        spawns[qPosition].gameObject.SetActive(true);
        qPosition++;
        }else{
            Instantiate(penguinPrefab,tmpPoint,Quaternion.identity);
        }
    }
    void SpawnArrow()
    {
        if (spawns != null)
        {
            spawns[qPosition].transform.position = tmpPoint;

            //This number needs to scale 
            tmpPoint.x += 1;
            tmpPoint.y += 1;
            spawns[qPosition + 1].transform.position = tmpPoint;
            tmpPoint.y -= 2;
            spawns[qPosition + 2].transform.position = tmpPoint;

            spawns[qPosition].gameObject.SetActive(true);
            spawns[qPosition + 1].gameObject.SetActive(true);
            spawns[qPosition + 2].gameObject.SetActive(true);

            qPosition += 3;
        }else{
            Vector3 t1 = tmpPoint, t2 = tmpPoint;
            t1.x = t2.x = tmpPoint.x + 1;
            t1.y -= 1;
            t2.y += 1;
            Instantiate(penguinPrefab, tmpPoint, Quaternion.identity);
            Instantiate(penguinPrefab, t1, Quaternion.identity);
            Instantiate(penguinPrefab, t2, Quaternion.identity);

        }

    }
    void SpawnPair()
    {
        if (spawns != null)
        {
            //This number needs to scale 
            tmpPoint.x += 1;
            spawns[qPosition].transform.position = tmpPoint;
            tmpPoint.x -= 2;;
            spawns[qPosition + 1].transform.position = tmpPoint;

            spawns[qPosition].gameObject.SetActive(true);
            spawns[qPosition + 1].gameObject.SetActive(true);

            qPosition += 2;
        }else{
            Instantiate(penguinPrefab, tmpPoint, Quaternion.identity);
            ++tmpPoint.x;
            Instantiate(penguinPrefab,tmpPoint,Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
            Spawn();
    }
}
