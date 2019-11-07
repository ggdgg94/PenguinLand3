using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
   public void playGame()
    {
        SceneManager.LoadScene("Mission1");

    }

    public void LoadGame1()
    {
        SceneManager.LoadScene("1");

    }
    public void LoadGame2()
    {
        SceneManager.LoadScene("2");

    }


    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
