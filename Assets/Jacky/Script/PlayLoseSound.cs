using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoseSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlaySound("Lose1");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
