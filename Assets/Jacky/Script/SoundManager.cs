using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip Shooting1, Shooting2, Walk1;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        Shooting1 = Resources.Load<AudioClip>("Shooting1");
        Walk1 = Resources.Load<AudioClip>("Walk1");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))           
        {
            Debug.Log("shootttttttttttttt");
            PlaySound("Shooting1");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("shootttttttttttttt");
            PlaySound("Walk1");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("shootttttttttttttt");
            PlaySound("Walk1");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("shootttttttttttttt");
            PlaySound("Walk1");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("shootttttttttttttt");
            PlaySound("Walk1");
        }
    }

    public static void PlaySound (string clip)
    {   
        switch (clip)
        {
            case "Shooting1":
                audioSrc.PlayOneShot(Shooting1);
                break;
            case "Walk1":
                audioSrc.PlayOneShot(Walk1);
                break;
        }
    }


}
