using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleGameManager : MonoBehaviour
{

    [Header("Characters")]
    //Prefabs set in Editor
    [SerializeField]
    Player playerPrefab;

    [Header("Menus")]
    [SerializeField]
    GameObject pauseUI;
    bool paused = false;
    [SerializeField]
    GameObject loseUI;
    [SerializeField]
    GameObject WinScoreUI;
    [SerializeField]
    GameObject WinTimeUI;

    [Header("Game UI")]
    [SerializeField]
    Image hearts;
    [SerializeField]
    Sprite[] playerLife;
    [SerializeField]
    Text stageTimer;
    static float timerTime = 60f; //for now we don't care about counting up
    [SerializeField]
    Text scoreText; //might get rid of this later to account for all types of penguins


    [Header("Level Settings")]
    //Do we want to play a level type? Keep both false for a general purpose scene
    public bool winByTime = false;
    public float timeForRound = 0f;

    public bool winByFeed = false;
    public int feedWinCondition = 0;


    [Header("General Scene Settings")]
    //General Purpose
    public bool generateCharacter = false;
    public bool countdownTime = false;

    [Header("Advanced Scene Settings")]
    public PenguinSpawner[] penguinSpawners;

    /* 
    public float[] penguinSpawnTimes = new float[4];
    public float[] itemSpawnTimes = new float[4];
    public Vector3[] spawnPoints = new Vector3[8];
   */

    Player player;
    Vector3 spawnPoint = new Vector3(6, -2, 0);
    PenguinSpawner[] horde;
    public static int score = 0;

    void CharacterSetup()
    {
        if (generateCharacter)
        {
            player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        }
        else
        {
            hearts.sprite = null;
            hearts.color = new Color(255, 255, 255, 0);
        }
    }

    void TimeSetup()
    {
        if (countdownTime)
        {
            timerTime = timerTime < 0 ? 60 : timerTime;
        }
        else
        {

        }

    }

    void SpawnSetup()
    {
        if (penguinSpawners.Length != 0)
        {
            horde = new PenguinSpawner[penguinSpawners.Length];
            for (int i = 0; i < penguinSpawners.Length; ++i)
            {

                horde[i] = Instantiate(penguinSpawners[i]); //prevent spawn before game starts
                horde[i].active = false;
            }
        }
    }

    void SpawnStart()
    {
        if (penguinSpawners != null)
        {
            for (int i = 0; i < penguinSpawners.Length; ++i)
            {
                horde[i].active = true; //prevent spawn before game starts
            }
        }
    }
    public void GeneralSetup()
    {
        CharacterSetup();
        TimeSetup();
        SpawnSetup();
    }

    public void FeedWinSetup()
    {
        if (winByFeed)
        {
            generateCharacter = true;
            feedWinCondition = feedWinCondition < 0 ? 10 : feedWinCondition;
            countdownTime = false;
            CharacterSetup();
            SpawnSetup();

        }
    }

    void TimeWinSetup()
    {
        generateCharacter = true;
        timeForRound = timeForRound < 0 ? 60f : timeForRound;
        countdownTime = true;
        CharacterSetup();
        TimeSetup();
        timerTime = timeForRound < 0 ? 60 : timeForRound;
        SpawnSetup();

    }
    void Start()
    {
        Time.timeScale = 1f;
        if (winByFeed)
        {
            FeedWinSetup();
        }
        else if (winByTime)
        {
            TimeWinSetup();
        }
        else
        {
            if (generateCharacter)
                GeneralSetup();
            if (countdownTime)
                TimeSetup();

            SpawnSetup();
        }
        SpawnStart();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePause();
        if (winByFeed)
        {
            UpdatePlayerLife();
            CheckGameState();
            UpdateScore();
            CheckScoreWin();
        }
        else if (winByTime)
        {
            UpdateTime();
            CheckTimedWin();
            UpdatePlayerLife();
            CheckGameState();
            UpdateScore();

        }
        else
        {
            if (generateCharacter)
            {
                UpdatePlayerLife();
            }
            if (countdownTime)
                UpdateTime();

            if (penguinSpawners != null)
                UpdateScore();
        }
    }
    void CheckGameState()
    {
        if (player.state == Character.CharacterState.Defeated)
        {
            loseUI.gameObject.SetActive(true);
            Time.timeScale = 0f; //psuedo pause
        }
    }

    public void Resume()
    {
        paused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Daniel-Test");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }

    void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    void UpdatePlayerLife()
    {
        hearts.sprite = playerLife[player.life];
    }
    void UpdateScore()
    {
        scoreText.text = " " + score;
    }

    void UpdateTime()
    {
        if (timerTime > 0)
        {
            timerTime -= 1 * Time.deltaTime;
            stageTimer.text = timerTime.ToString("0");
        }
    }

    void CheckTimedWin()
    {
        if (timerTime <= 0)
        {
            Debug.Log("You Win!");
            Time.timeScale = 0f;
            WinTimeUI.SetActive(true);

        }
    }

    void CheckScoreWin()
    {
        if (score >= feedWinCondition)
        {
            Debug.Log("You win!!!!");
            Time.timeScale = 0f;
            WinScoreUI.SetActive(true);

        }
    }

    void ChangeScenes()
    {

    }
}