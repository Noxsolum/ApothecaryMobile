using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public int redScore = 0, blueScore = 0, yellowScore = 0;
    public object redCube, blueCube, yellowCube;

    void Start()
    {

        //scoreText.text = "Score: " + scoreNo;
        scoreText = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //RaycastHit hit;
        //    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    //if (Physics.Raycast (ray, out hit))
        //    //{
        //    //    if(hit.transform.name == "RedCube")
        //    //    {
        //    //        redScore++;
        //    //    }
        //    //    else if (hit.transform.name == "BlueCube")
        //    //    {
        //    //        blueScore++;
        //    //    }

        //    //}
        //}
        if (Input.GetKeyDown("space"))
        {
            if (Input.GetKeyDown("1"))
            {
                redScore++;
            }
        }
        scoreText.text = "Red Score: " + redScore;
    
    }
}
