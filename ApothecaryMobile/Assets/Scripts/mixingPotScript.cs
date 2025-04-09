using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixingPotScript : MonoBehaviour
{
    public List<int> containedIngredients = new List<int>();
    private string[] potionArray = new string[15];
    private Vector3 dropLoc = new Vector3(0f, 0f, 0f);
    public bool brewTimer = false;
    float fiveSec = 5;
    private soundScript potionSound;
    PotionScript PTScr;
    public BackgroundScript backgroundScript;

    public void Awake()
    {
        potionArray[0] = "Green_Tall";
        potionArray[1] = "Red_Tall";
        potionArray[2] = "Yellow_Tall";
        potionArray[3] = "Purple_Tall";
        potionArray[4] = "Blue_Tall";
        potionArray[5] = "Green_Round";
        potionArray[6] = "Red_Round";
        potionArray[7] = "Yellow_Round";
        potionArray[8] = "Purple_Round";
        potionArray[9] = "Blue_Round";
        potionArray[10] = "Green_Tri";
        potionArray[11] = "Red_Tri";
        potionArray[12] = "Yellow_Tri";
        potionArray[13] = "Red_Tri";
        potionArray[14] = "Blue_Tall";

        PTScr = GameObject.Find("Main Camera").GetComponent<PotionScript>();
        backgroundScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BackgroundScript>();
        potionSound = GameObject.FindWithTag("Audio").GetComponent<soundScript>();
    }

    private void AssignVariables()
    {

    }

    public int returnListSize()
    {
        return containedIngredients.Count;
    }

    public int returnFirstIngre()
    {
        if(containedIngredients.Count == 0)
        {
            return 0;
        }
        else
        {
            return containedIngredients[0];
        }
    }

    public void inputItem(int newItem)
    {
        containedIngredients.Add(newItem);
        Debug.Log("New Ingredient " + newItem + " added to the pot called " + this.name);
    }


    public void outputPotion()
    {
        dropLoc = new Vector3(this.transform.position.x, this.transform.position.y - 2, 0f);
        int newPotion = 0;

        newPotion = containedIngredients[0] * containedIngredients[1];
        Debug.Log(newPotion + " = " + containedIngredients[0] + " + " + containedIngredients[1]);

        Instantiate(PTScr.ReturnObj(newPotion), dropLoc, new Quaternion(0, 0, 0, 0));

        Debug.Log("Name of Potion Created : " + PTScr.ReturnObj(newPotion).name);
        containedIngredients.Clear();

        //Instantiate(Resources.Load("PotionPrefabs/" + potionArray[x][y]), dropLoc, new Quaternion(0, 0, 0, 0));
        //Debug.Log(potionArray);
        Debug.Log("Pot " + this.name + " has just output a potion!");
    }


    void Update()
    {
        if (brewTimer == true && !backgroundScript.isPaused)
        {
            Debug.Log("StartTimer");
            fiveSec -= Time.deltaTime;
            if (fiveSec <= 0)
            {
                GetComponent<Animator>().SetTrigger("NewAnim");
                Debug.Log("EndTimer");
                fiveSec = 5;
                brewTimer = false;
                potionSound.soundArray(5, 2);
                outputPotion();
            }
        }
    }
}
