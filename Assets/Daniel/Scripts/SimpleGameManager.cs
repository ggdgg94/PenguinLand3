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
    [SerializeField]
    Character regularPenguin;
    [SerializeField]
    Character slidingPenguin;
    [SerializeField]
    Character babyPenguin;
    [SerializeField]
    Character bigPenguin;

    [Header("Items")]
    [SerializeField]
    Item regularBullet;
    [SerializeField]
    Item longBullet;
    [SerializeField]
    Item bigBullet;
    [SerializeField]
    Item specialBullet;

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

    //prepare to keep track of all types of penguins
    public enum PenguinType{
        Regular = 0,
        Baby,
        Sliding,
        Big
    };
    //int[] penguinsFed = new int[4]
    public static int score = 0;

    Player player; 
    Vector3 spawnPoint = new Vector3(6,-2,0);
    Character[] horde;
     

    [Header("Level Settings")]
    //Do we want to play a level type? Keep both false for a general purpose scene
    public bool winByTime = false;
    public float timeForRound = 0f;

    public bool winByFeed = false;
    public int feedWinCondition = 0;



    [Header("General Scene Settings")]
    //General Purpose
    public bool generateCharacters = false;
    public bool countdownTime = false;
    public int numPenguins = 0; 
    public float[] penguinSpawnTimes = new float[4];
    public float[] itemSpawnTimes = new float[4];


    [Header("Demo")]
    public bool demo1 = false;
    public bool demo2 = false;
    public bool demo3 = false;

    void Start()
    {
        Time.timeScale = 1f;

        if(demo1){
            SetUpPlayer();
            SetUpPlayerPara(0,0,0,0);
            SetUpPenguinPara(regularPenguin, 0,0,0,0);
            timerTime = 15f;

        }else if(demo2){

        }else if(demo3){

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
    }

    void SetUpPlayer()
    {
        player = Instantiate(playerPrefab);
        player.gameObject.SetActive(true);
    }
    void SetUpPenguinAmount(Character penguin, int amount = 30)
    {
        horde = new Character[amount];
        for(int i = 0; i < horde.Length; ++i){
            horde[i] = Instantiate(penguin);
            horde[i].gameObject.SetActive(false);
        }
    }

    void SpawnRandoms()
    {

    }



    /*LEVEL 1 - WIN BY TIME */
    float spawnr = 0.3f;
    float spawns = 0.5f;
    float spawnb = 0.4f;
    float spawnf = 0.9f;
    Vector3 spawnRPoint = new Vector3(22f, 0f, 0f);
    void SetUpPlayerPara(float x1, float x2, float y1, float y2)
    {
        player.minX = x1;
        player.maxX = x2;
        player.minY = y1;
        player.maxY = y2;
    }
    void SetUpPenguinPara(Character p, float x1, float x2, float y1, float y2)
    {
        p.minX = x1;
        p.maxX = x2;
        p.minY = y1;
        p.maxY = y2;

    }
    void SpawnRegulars()
    {
        spawnr -= Time.deltaTime;
        if(spawntime < 0){
            spawnPoint.y = Random.Range(player.minY, player.maxY); //may need to set max and min manually
            spawnr = 0.3f;
            Instantiate(regularPenguin, spawnRPoint, Quaternion.identity);
        }       
    }

    void Level1()
    {
        SetUpPlayer();
        
    }


    float spawntime = 0.3f;
    void Spawn(Character penguin)
    {
        spawntime -= Time.deltaTime;
        if(spawntime < 0){
            spawnPoint.x = Random.Range(player.minX, player.maxX);
            spawnPoint.y = Random.Range(player.minY, player.maxY);
            spawntime = 0.3f;
            Instantiate(penguin, spawnPoint, Quaternion.identity);
        }
    }
    int num = 0;
    void Spawn2(float timer = 0.4f)
    {
        spawntime -= Time.deltaTime;
        if(spawntime < 0 && num < horde.Length){
            spawnPoint.x = Random.Range(player.minX, player.maxX);
            spawnPoint.y = Random.Range(player.minX, player.maxX);
            horde[num].transform.position = spawnPoint;

            spawnPoint.x += 1;
            spawnPoint.y += 2;
            horde[num+1].transform.position = spawnPoint;
            spawnPoint.y -= 4;
            horde[num+2].transform.position = spawnPoint;


            horde[num].gameObject.SetActive(true);
            horde[num+1].gameObject.SetActive(true);
            horde[num+2].gameObject.SetActive(true);
            spawntime = timer;
            num+=3;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(demo1){
            HandlePause();
            UpdateTime();
            SpawnRegulars();
            UpdatePlayerLife();
            UpdateScore();
            CheckGameState();

        }

        if(demo2){

        }

        if(demo3){

        }

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

        }
    }

    void ChangeScenes()
    {

    }
}
