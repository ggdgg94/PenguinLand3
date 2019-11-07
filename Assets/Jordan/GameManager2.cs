using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public int mNumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
    public float mStartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
    public float mEndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
    public GameObject playerPrefab;             // Reference to the prefab the players will control.
    public PenguinManager[] mTanks;               // A collection of managers for enabling and disabling different aspects of the tanks.
    public Text mMessageText;                  // Reference to the overlay Text to display winning text, etc.


    private int _mRoundNumber;                  // Which round the game is currently on.
    private bool _mRoundWon = false;                 // Which round the game is currently on.
    private WaitForSeconds _mStartWait;         // Used to have a delay whilst the round starts.
    private WaitForSeconds _mEndWait;           // Used to have a delay whilst the round or game ends.
    private bool _mGameWon = false;


    private void Start()
    {
        // Create the delays so they only have to be made once.
        _mStartWait = new WaitForSeconds (mStartDelay);
        _mEndWait = new WaitForSeconds (mEndDelay);

        SpawnAllTanks();

        // Once the tanks have been created and the camera is using them as targets, start the game.
        StartCoroutine (GameLoop ());
    }


    private void SpawnAllTanks()
    {
        // For all the tanks...
        for (int i = 0; i < mTanks.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            mTanks[i].m_Instance = Instantiate(mTankPrefab, mTanks[i].m_SpawnPoint.position, mTanks[i].m_SpawnPoint.rotation) as GameObject;
            mTanks[i].Setup();
        }
    }


    // This is called from start and will run each phase of the game one after another.
    private IEnumerator GameLoop ()
    {
        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
        yield return StartCoroutine (RoundStarting ());

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine (RoundPlaying());

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        yield return StartCoroutine (RoundEnding());

        // This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
        if (_mRoundWon != true)
        {
            // If there is a game winner, restart the level.
            Application.LoadLevel (Application.loadedLevel);
        }
        else
        {
            // If there isn't a winner yet, restart this coroutine so the loop continues.
            // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
            StartCoroutine (GameLoop ());
        }
    }


    private IEnumerator RoundStarting ()
    {
        // As soon as the round starts reset the tanks and make sure they can't move.
        ResetAllTanks ();
        DisableTankControl ();

        // Increment the round number and display text showing the players what round it is.
        _mRoundNumber++;
        mMessageText.text = "ROUND " + _mRoundNumber;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return _mStartWait;
    }


    private IEnumerator RoundPlaying ()
    {
        // As soon as the round begins playing let the players control the tanks.
        EnableTankControl ();

        // Clear the text from the screen.
        mMessageText.text = string.Empty;

        // While there is not one tank left...
        while (!OneTankLeft())
        {
            // ... return on the next frame.
            yield return null;
        }
    }


    private IEnumerator RoundEnding ()
    {
        // Stop tanks from moving.
        DisableTankControl ();

        // Clear the winner from the previous round.
        _mRoundWinner = null;

        // See if there is a winner now the round is over.
        _mRoundWinner = GetRoundWinner ();

        // If there is a winner, increment their score.
        if (_mRoundWinner != null)
            _mRoundWinner.m_Wins++;

        // Now the winner's score has been incremented, see if someone has one the game.
        _mGameWinner = GetGameWinner ();

        // Get a message based on the scores and whether or not there is a game winner and display it.
        string message = EndMessage ();
        mMessageText.text = message;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return _mEndWait;
    }


    // This is used to check if there is one or fewer tanks remaining and thus the round should end.
    private bool OneTankLeft()
    {
        // Start the count of tanks left at zero.
        int numTanksLeft = 0;

        // Go through all the tanks...
        for (int i = 0; i < mTanks.Length; i++)
        {
            // ... and if they are active, increment the counter.
            if (mTanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        // If there are one or fewer tanks remaining return true, otherwise return false.
        return numTanksLeft <= 1;
    }


    // Returns a string message to display at the end of each round.
    private string EndMessage()
    {
        // By default when a round ends there are no winners so the default end message is a draw.
        string message = "WOOO!";

        // If there is a winner then change the message to reflect that.
        if (_mRoundWon != null)
            message = "Round Won!";

        // Add some line breaks after the initial message.
        // message += "\n\n\n\n";

        // If there is a game winner, change the entire message to reflect that.
        if (_mGameWon == true)
            message = "Game Won!";

        return message;
    }


    // This function is used to turn all the tanks back on and reset their positions and properties.
    private void ResetAllTanks()
    {
        for (int i = 0; i < mTanks.Length; i++)
        {
            mTanks[i].Reset();
        }
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < mTanks.Length; i++)
        {
            mTanks[i].EnableControl();
        }
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < mTanks.Length; i++)
        {
            mTanks[i].DisableControl();
        }
    }
}
