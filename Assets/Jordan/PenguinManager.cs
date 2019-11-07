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
    public int mPlayerNumber;            // This specifies which player this the manager for.
    public GameObject mInstance;         // A reference to the instance of the tank when it is created.


//    private TankMovement _mMovement;                        // Reference to tank's movement script, used to disable and enable control.
//    private TankShooting _mShooting;                        // Reference to tank's shooting script, used to disable and enable control.
    private GameObject _mCanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.

    
    public void Setup ()
    {
        // Get references to the components.
        _mMovement = mInstance.GetComponent<TankMovement> ();
        m_Shooting = mInstance.GetComponent<TankShooting> ();
        _mCanvasGameObject = mInstance.GetComponentInChildren<Canvas> ().gameObject;

        // Set the player numbers to be consistent across the scripts.
        _mMovement.m_PlayerNumber = mPlayerNumber;
        m_Shooting.m_PlayerNumber = mPlayerNumber;

        // Get all of the renderers of the tank.
        MeshRenderer[] renderers = mInstance.GetComponentsInChildren<MeshRenderer> ();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = m_PlayerColor;
        }
    }


    // Used during the phases of the game where the player shouldn't be able to control their tank.
    public void DisableControl ()
    {
        _mMovement.enabled = false;
        _mShooting.enabled = false;

        _mCanvasGameObject.SetActive (false);
    }


    // Used during the phases of the game where the player should be able to control their tank.
    public void EnableControl ()
    {
        _mMovement.enabled = true;
        _mShooting.enabled = true;

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
