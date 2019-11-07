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
    static float startingTime = 60f; //for now we don't care about counting up
    [SerializeField]
    Text scoreText; //might get rid of this later to account for all types of penguins

    //prepare to keep track of all types of penguins
    public enum PenguinType{
        Regular,
        Baby,
        Sliding,
        Big
    };
    //int[] penguinsFed = new int[4]
    public static int score = 0;

    Player player; 
    Vector3 spawnPoint = new Vector3(6,-2,0);
    Character[] horde;

    public bool generateCharacters = false;
    public bool countdownTime = false;
    void Start()
    {
        Time.timeScale = 1f;
        if(generateCharacters){
            player = Instantiate(playerPrefab);
            player.gameObject.SetActive(true);
            horde = new Character[30];
            for(int i = 0; i < horde.Length; ++i){
                horde[i] = Instantiate(regularPenguin);
                horde[i].gameObject.SetActive(false);
            }
        }else{
            hearts.sprite = null;
            hearts.color = new Color(255, 255, 255, 0);
        }
        
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

        HandlePause();
        if(generateCharacters){
            Spawn2();
            UpdatePlayerLife();
            UpdateScore();
            CheckGameState();
        }
        if(countdownTime){
            if(startingTime > 0){
                startingTime -= 1 * Time.deltaTime;
                stageTimer.text = startingTime.ToString("0");
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
}
