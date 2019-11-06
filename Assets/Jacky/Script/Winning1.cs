using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Winning1 : MonoBehaviour
{

    public GameObject Win1UI;
    public bool win1 = false;


    void Start()
    {
        Win1UI.SetActive(false);
    }

     void Update()
    {

        if (ScoreScript.scoreValue == 15)
        {   
            Win1UI.SetActive(true);
            Time.timeScale = 0.00001f;

            //SceneManager.LoadScene("2");
        }
        else
        {
            Time.timeScale = 1;

        }
    }
    public void Nextlevel()
    {
        SceneManager.LoadScene("2");
    }
}

 
