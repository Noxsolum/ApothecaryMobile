using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixingPotScript : MonoBehaviour
{
    List<string> containedIngredients = new List<string>();
    private string[] potionArray = new string[15];
    private Vector3 dropLoc = new Vector3(0f, 0f, 0f);
    public bool brewTimer = false;
    float fiveSec = 5;

    public void Awake()
    {
        AssignVariables();
    }

    private void AssignVariables()
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
    }

    public int returnListSize()
    {
        return containedIngredients.Count;
    }

    public string returnFirstIngre()
    {
        if(containedIngredients.Count == 0)
        {
            return "Empty";
        }
        else
        {
            return containedIngredients[0];
        }
    }

    public void inputItem(GameObject newItem)
    {
        containedIngredients.Add(newItem.name);
        Debug.Log("New Ingredient " + newItem + " added to the pot called " + this.name);
    }


    public void outputPotion()
    {
        if(containedIngredients[0] == "Refined Mushroom" && containedIngredients[1] == "Refined Berries" || containedIngredients[1] == "Refined Mushroom" && containedIngredients[0] == "Refined Berries")
        {
            Instantiate(Resources.Load("PotionPrefabs/" + potionArray[0]), dropLoc, new Quaternion(0, 0, 0, 0));
        }
        else if (containedIngredients[0] == "Refined Mushroom" && containedIngredients[1] == "Refined Radish" || containedIngredients[1] == "Refined Mushroom" && containedIngredients[0] == "Refined Radish")
        {
            Instantiate(Resources.Load("PotionPrefabs/" + potionArray[1]), dropLoc, new Quaternion(0, 0, 0, 0));
        }
        else if (containedIngredients[0] == "Refined Mushroom" && containedIngredients[1] == "Refined Sage" || containedIngredients[1] == "Refined Mushroom" && containedIngredients[0] == "Refined Sage")
        {
            Instantiate(Resources.Load("PotionPrefabs/" + potionArray[2]), dropLoc, new Quaternion(0, 0, 0, 0));
        }
        else if (containedIngredients[0] == "Refined Mushroom" && containedIngredients[1] == "Refined Lavinder" || containedIngredients[1] == "Refined Mushroom" && containedIngredients[0] == "Refined Lavinder")
        {
            Instantiate(Resources.Load("PotionPrefabs/" + potionArray[3]), dropLoc, new Quaternion(0, 0, 0, 0));
        }
        else if (containedIngredients[0] == "Refined Mushroom" && containedIngredients[1] == "Refined Snowdrop" || containedIngredients[1] == "Refined Mushroom" && containedIngredients[0] == "Refined Snowdrop")
        {
            Instantiate(Resources.Load("PotionPrefabs/" + potionArray[4]), dropLoc, new Quaternion(0, 0, 0, 0));
        }

        Debug.Log("....");
        int x = 0, y = 0, z = 0;
        for(int i = 0; i < containedIngredients.Count; i++)
        {
            switch(containedIngredients[i])
            {
                case "Refined Mushroom":
                    if (i == 0) { x = 0; }
                    else if (i == 1) { y = 0; }
                    break;
                case "Refined Berries":
                    if (i == 0) { x = 1; }
                    else if (i == 1) { y = 1; }
                    break;
                case "Refined Radish":
                    if (i == 0) { x = 2; }
                    else if (i == 1) { y = 2; }
                    break;
                case "Refined Sage":
                    if (i == 0) { x = 3; }
                    else if (i == 1) { y = 3; }
                    break;
                case "Refined Lavinder":
                    if (i == 0) { x = 4; }
                    else if (i == 1) { y = 4; }
                    break;
                case "Refined Snowdrop":
                    if (i == 0) { x = 5; }
                    else if (i == 1) { y = 5; }
                    break;
                default:
                    break;
            }
        }

        containedIngredients.Clear();
        dropLoc = new Vector3(this.transform.position.x, this.transform.position.y - 2, 0f);
        Instantiate(Resources.Load("PotionPrefabs/" + potionArray[x][y]), dropLoc, new Quaternion(0, 0, 0, 0));
        Debug.Log(potionArray);
        Debug.Log("Pot " + this.name + " has just output a potion!");
    }


    void Update()
    {
        Debug.Log("adsfasdf");
        if (brewTimer == true)
        {
            Debug.Log("StartTimer");
            fiveSec -= Time.deltaTime;
            if (fiveSec <= 0)
            {
                Debug.Log("EndTimer");
                fiveSec = 5;
                brewTimer = false;
                outputPotion();
            }
        }
    }
}
