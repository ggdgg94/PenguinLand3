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

    [Header("Game UI")]
    [SerializeField]
    Image hearts;
    [SerializeField]
    Sprite[] playerLife ;
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
    Vector3 spawnPoint = new Vector3(6,-2,0);
    Character[] horde;
    public static int score = 0;

    public void GeneralSetup()
    {
        if(generateCharacter){
            player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        }else{
            hearts.sprite = null;
            hearts.color = new Color(255, 255, 255, 0);
        }
        
    }
    void Start()
    {
        Time.timeScale = 1f;
        GeneralSetup();

/* 

        }else{
            Time.timeScale = 1f;
            if(winByTime){
                generateCharacters = true;
                countdownTime = true;
                SetUpPlayer();

            }else if(winByFeed){
                generateCharacters = true;
                SetUpPlayer();

            }else{
                if(generateCharacters){
                    SetUpPlayer();
                    SetUpPenguinAmount(regularPenguin, 30);
                }else{
                    hearts.sprite = null;
                    hearts.color = new Color(255, 255, 255, 0);
                }
            }
        }
        Debug.Log(player.minX);
*/
    }

    void SetUpPlayer()
    {
        player = Instantiate(playerPrefab);
    }
    void SetUpPlayerPara(float x1, float x2, float y1, float y2)
    {
        player.minX = x1;
        player.maxX = x2;
        player.minY = y1;
        player.maxY = y2;
    }

    // Update is called once per frame
    void Update()
    {
        HandlePause();
        /*

        if(!demo1 && !demo2 && !demo3){
            HandlePause();
            if(generateCharacters){
                Spawn2();
                UpdatePlayerLife();
                UpdateScore();
                CheckGameState();
            }
            if(countdownTime){
                UpdateTime();
            
            }
        }
        */
        
        //Spawn(regularPenguin);

        
    }
    void CheckGameState()
    {
        if(player.state == Character.CharacterState.Defeated){
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
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
            if(paused){
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
        if(timerTime > 0){
            timerTime -= 1 * Time.deltaTime;
            stageTimer.text = timerTime.ToString("0");
        }
    }

    void CheckTimedWin()
    {
        if(timerTime <= 0){
            Debug.Log("You Win!");

        }
    }

    void CheckScoreWin()
    {
        if(score >= feedWinCondition){

        }
    }

    void ChangeScenes()
    {

    }
}
