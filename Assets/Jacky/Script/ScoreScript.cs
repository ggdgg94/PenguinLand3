using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;
    int initalScore = 0;
    [SerializeField] Text score;




    void Start()
    {
        scoreValue = initalScore;
    }

    // Update is called once per frame
    void Update()
    {
        score = GetComponent<Text>();
        score.text = " " + scoreValue;
    }
}
