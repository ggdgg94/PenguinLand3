﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float DestoryAfterTime = 1.0f;

    void Start()
    {
        Destroy(gameObject, DestoryAfterTime);
    }
}