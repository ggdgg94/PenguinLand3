using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    private bool start=true;
    public GameObject Stage1UI;
    // Start is called before the first frame update
    void Start()
    {
            Stage1UI.SetActive(true);   
    }

    void Update()
    {
        if(start==true)
        {
            Time.timeScale = 0;
        }
    }
    public void Resume()
    {

        Time.timeScale = 1;
        Stage1UI.SetActive(false);

    }
    // Update is called once per frame

}
