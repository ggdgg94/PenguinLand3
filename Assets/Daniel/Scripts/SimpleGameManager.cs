﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("UI")]
    [SerializeField]
    GameObject pauseUI;
    bool paused = false;
    [SerializeField]
    GameObject loseUI;

    Player player; 
    Vector3 spawnPoint = new Vector3(6,-2,0);
    Character[] horde;
    void Start()
    {
        player = Instantiate(playerPrefab);
        player.gameObject.SetActive(true);
        horde = new Character[30];
        for(int i = 0; i < horde.Length; ++i){
            horde[i] = Instantiate(regularPenguin);
            horde[i].gameObject.SetActive(false);
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
        Spawn2();
        HandlePause();
        CheckGameState();
        
        //Spawn(regularPenguin);

        
    }
    void CheckGameState()
    {
            Debug.Log("CALLED" + player.state);
        if(player.state == Character.CharacterState.Defeated){
            Debug.Log("CALLED");
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
        SceneManager.LoadScene("Game");
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
}
