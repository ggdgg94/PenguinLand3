using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Winning1 : MonoBehaviour
{

    public GameObject Win1UI;
    public bool win1 = false;
    public GameObject Player;

    void Start()
    {
        Win1UI.SetActive(false);
    }

     void Update()
    {

        if (ScoreScript.scoreValue == 40)
        {   
            Win1UI.SetActive(true);
            Time.timeScale = 0.00001f;
            PlayerGone();
            //SceneManager.LoadScene("2");
        }
        else
        {
            Time.timeScale = 1;

        }

    }
    public void Nextlevel()
    {
        SceneManager.LoadScene("Mission2");
    }
    public void PlayerGone()
    {
        Player.SetActive(false);
    }
}

 
