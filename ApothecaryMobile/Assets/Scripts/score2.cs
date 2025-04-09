//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class score2 : MonoBehaviour
{
    // Start is called before the first frame update
    Text scoreText;
    public int redScore = 0, blueScore = 0, yellowScore = 0, greenScore = 0, purpleScore = 0, orangeScore = 0;
    public object redCube, blueCube, yellowCube;

    void Start()
    {
        //scoreText.text = "Score: " + scoreNo;
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (redScore < 5)
                redScore++;
        }

        if (Input.GetKeyDown("2"))
        {
            if (blueScore < 5)
                blueScore++;
        }

        if (Input.GetKeyDown("3"))
        {
            if (yellowScore < 5)
                yellowScore++;
        }

        if (Input.GetKeyDown("q")) //Green
        {
            if (blueScore >= 1 && yellowScore >= 1)
            {
                greenScore++;
                blueScore--;
                yellowScore--;
            }
            else
                Debug.Log("Not enough resources!");
        }

        if (Input.GetKeyDown("w")) //Purple
        {
            if (blueScore >= 1 && redScore >= 1)
            {
                purpleScore++;
                blueScore--;
                redScore--;
            }
            else
                Debug.Log("Not enough resources!");
        }

        if (Input.GetKeyDown("e")) //Orange
        {
            if (redScore >= 1 && yellowScore >= 1)
            {
                orangeScore++;
                redScore--;
                yellowScore--;
            }
            else
                Debug.Log("Not enough resources!");
        }
        scoreText.text = "Red Score: " + redScore + "\nBlue Score: " + blueScore + "\nYellow Score: " + yellowScore + "\nGreen Score: " + greenScore + "\nPurple Score: " + purpleScore + "\nOrange Score: " + orangeScore;
    }
}