using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Winning2 : MonoBehaviour
{
    public GameObject Win2UI;
    public bool win1 = false;


    void Start()
    {
        Win2UI.SetActive(false);
    }

    void Update()
    {

        if (CountdownTimer.currentTime == 0)
        {
            Win2UI.SetActive(true);
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
        SceneManager.LoadScene("3");
    }
}
