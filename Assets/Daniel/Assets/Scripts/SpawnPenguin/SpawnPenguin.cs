using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPenguin : MonoBehaviour
{
    float speed;
    public GameObject enemy;
    public float xPositionLmit;
    public float yPositionLimit;
    public float SpawnRate;

    void Start()
    {
        speed = 2f;
    }
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToScreenPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }

    }
}
