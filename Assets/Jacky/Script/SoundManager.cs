using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip Shooting1, Shooting2, Shooting3, Walk1, Win2,Lose1,GetHit0, GetHit1, GetHit2, FlyAway1, Dashing1;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        Shooting1 = Resources.Load<AudioClip>("Shooting1");
        Shooting2 = Resources.Load<AudioClip>("Shooting2");
        Shooting3 = Resources.Load<AudioClip>("Shooting3");
        Dashing1 = Resources.Load<AudioClip>("Dashing1");
        GetHit0 = Resources.Load<AudioClip>("GetHit0");
        GetHit1 = Resources.Load<AudioClip>("GetHit1");
        GetHit2 = Resources.Load<AudioClip>("GetHit2");
        FlyAway1 = Resources.Load<AudioClip>("FlyAway1");
        Lose1= Resources.Load<AudioClip>("Lose1");
        Walk1 = Resources.Load<AudioClip>("Walk1");

        Win2= Resources.Load<AudioClip>("Win2");
        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Dashinggggggggg");
            PlaySound("Dashing1");
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("shootttttttttttttt");
            PlaySound("Shooting1");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("shootttttttttttttt");
            PlaySound("Walk1");
        }
        
    }

    
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Shooting1":
                audioSrc.PlayOneShot(Shooting1);
                break;
            case "Shooting2":
                audioSrc.PlayOneShot(Shooting2);
                break;
            case "Shooting3":
                audioSrc.PlayOneShot(Shooting3);
                break;
            case "Dashing1":
                audioSrc.PlayOneShot(Dashing1);
                break;
            case "GetHit0":
                audioSrc.PlayOneShot(GetHit0);
                break;
            case "GetHit1":
                audioSrc.PlayOneShot(GetHit1);
                break;
            case "GetHit2":
                audioSrc.PlayOneShot(GetHit2);
                break;
            case "Walk1":
                audioSrc.PlayOneShot(Walk1);
                break;
            case "Win2":
                audioSrc.PlayOneShot(Win2);
                break;
            case "Lose1":
                audioSrc.PlayOneShot(Lose1);
                break;
            case "FlyAway1":
                audioSrc.PlayOneShot(FlyAway1);
                break;
        }
    }
    
 }
   