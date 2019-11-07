using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int mNumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
    public float mStartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
    public float mEndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
    public GameObject playerPrefab;
    public GameObject penguinPrefab;
    public Enemy[] mPenguins;
    public Text mMessageText;                  // Reference to the overlay Text to display winning text, etc.
    public Transform[] spawnPoints;
    public int penguinThreshold = 10;
    public int penguinsInVillage = 0;
    public float minY = 1;
    public float maxY = 5;
    public float minX = 1;
    public float maxX = 5;

    private int _mRoundNumber = 0;                  // Which round the game is currently on.
    private bool _mRoundWon = false;
    private WaitForSeconds _mStartWait;         // Used to have a delay whilst the round starts.
    private WaitForSeconds _mEndWait;           // Used to have a delay whilst the round or game ends.
    private bool _mGameWon = false;
    private float timer = 0.0f;
    private int NumOfPenguins = 10;

    
    public int playerHealth = 10;


    private void Start()
    {
        // Create the delays so they only have to be made once.
        _mStartWait = new WaitForSeconds (mStartDelay);
        _mEndWait = new WaitForSeconds (mEndDelay);

        SpawnPenguins();

        // Once the tanks have been created and the camera is using them as targets, start the game.
        StartCoroutine (GameLoop ());
    }


    private void SpawnPenguins()
    {
//        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
//        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        for (int i = 0; i < mPenguins.Length; i++)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);
            position.x = Random.Range(minX, maxX);
            position.y = Random.Range(minY, maxY);

//            mPenguins[i].mInstance = Instantiate(penguinPrefab, position, Quaternion.Euler(0f,0f,0f)) as GameObject;
//            mPenguins[i].Setup();
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
        if (_mRoundNumber >= mNumRoundsToWin)
        {
            // If there is a game winner, restart the level.
            SceneManager.LoadScene("Game");
        }
        else
        {
            // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
            StartCoroutine (GameLoop ());
        }
    }


    private IEnumerator RoundStarting ()
    {
        _mRoundNumber++;
        mMessageText.text = "ROUND " + _mRoundNumber.ToString();

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return _mStartWait;
    }


    private IEnumerator RoundPlaying ()
    {
        // Clear the text from the screen.
        mMessageText.text = string.Empty;

        // While player health > 0 or penguins passed < threshold
        while (!RoundOver())
        {
            timer += Time.deltaTime;
            Debug.Log("Time Elapsed: " + timer.ToString());

            // ... return on the next frame.
            yield return null;
        }
    }


    private IEnumerator RoundEnding ()
    {
        if (_mRoundWon == true)
        {
        }

        string message = EndMessage ();
        mMessageText.text = message;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return _mEndWait;
    }


    private bool RoundOver()
    {
        int pengsInVillage = 0;
        for (int i = 0; i < mPenguins.Length; i++)
        {
            // ... and if they are active, increment the counter.
            if (!mPenguins[i].mInstance.activeSelf)
                pengsInVillage++;
        }

        penguinsInVillage = pengsInVillage;
        if (playerHealth <= 0)
        {
            if (penguinsInVillage >= penguinThreshold)
            {
                return true;
            }
            
        }

        if (timer > 60)
        {
            _mRoundWon = true;
            return true;
        }

        return false;
    }


    // Returns a string message to display at the end of each round.
    private string EndMessage()
    {
        // By default when a round ends there are no winners so the default end message is a draw.
        string message = "WOOO!";

        // If there is a winner then change the message to reflect that.
        if (_mRoundWon == true)
            message = "Round Won!";

        // Add some line breaks after the initial message.
        // message += "\n\n\n\n";

        // If there is a game winner, change the entire message to reflect that.
        if (_mGameWon == true)
            message = "Game Won!";

        return message;
    }
}
