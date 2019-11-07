using System;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class PenguinManager : MonoBehaviour
{
    // This class is to manage various settings on a tank.
    // It works with the GameManager class to control how the tanks behave
    // and whether or not players have control of their tank in the 
    // different phases of the game.

    public Transform mSpawnPoint;                          // The position and direction the tank will have when it spawns.
    public GameObject mInstance;         // A reference to the instance of the tank when it is created.


//    private TankMovement _mMovement;                        // Reference to tank's movement script, used to disable and enable control.
//    private TankShooting _mShooting;                        // Reference to tank's shooting script, used to disable and enable control.
    private GameObject _mCanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.

    
    public void Setup ()
    {
        // Pass.
    }


    // Used during the phases of the game where the player shouldn't be able to control their tank.
    public void DisableControl ()
    {
        _mCanvasGameObject.SetActive (false);
    }


    // Used during the phases of the game where the player should be able to control their tank.
    public void EnableControl ()
    {
        _mCanvasGameObject.SetActive (true);
    }


    // Used at the start of each round to put the tank into it's default state.
    public void Reset ()
    {
        mInstance.transform.position = mSpawnPoint.position;
        mInstance.transform.rotation = mSpawnPoint.rotation;

        mInstance.SetActive (false);
        mInstance.SetActive (true);
    }
}
