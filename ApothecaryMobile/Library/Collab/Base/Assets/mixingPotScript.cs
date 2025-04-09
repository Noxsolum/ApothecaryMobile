using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixingPotScript : MonoBehaviour
{
    List<string> containedIngredients = new List<string>();
    private string[][] potionArray;
    private Vector3 dropLoc = new Vector3(0f, 0f, 0f);
    public bool brewTimer = false;
    float fiveSec = 5;

    public void Awake()
    {
        potionArray[0][1] = "Green_Tall";
        potionArray[0][2] = "Red_Tall";
        potionArray[0][3] = "Yellow_Tall";
        potionArray[0][4] = "Purple_Tall";
        potionArray[0][5] = "Blue_Round";
        potionArray[1][0] = "Green_Tall";
        potionArray[1][1] = "Red_Round";
        potionArray[1][2] = "Yellow_Round";
        potionArray[1][3] = "Purple_Round";
        potionArray[1][4] = "Blue_Tri";
        potionArray[1][5] = "Green_Tri";
        potionArray[2][0] = "Red_Tall";
        potionArray[2][1] = "Yellow_Round";
        potionArray[2][3] = "Green_Round";
        potionArray[2][4] = "Red_Tri";
        potionArray[2][5] = "Blue_Tall";
        potionArray[3][0] = "Yellow_Tall";
        potionArray[3][1] = "Purple_Round";
        potionArray[3][2] = "Green_Round";
        potionArray[3][4] = "Yellow_Tri";
        potionArray[3][5] = "Red_Round";
        potionArray[4][0] = "Purple_Tall";
        potionArray[4][1] = "Blue_Tri";
        potionArray[4][2] = "Red_Tri";
        potionArray[4][3] = "Yellow_Tri";
        potionArray[4][5] = "Purple_Tri";
        potionArray[5][0] = "Blue_Round";
        potionArray[5][1] = "Green_Tri";
        potionArray[5][2] = "Blue_Tall";
        potionArray[5][3] = "Red_Round";
        potionArray[5][4] = "Purple_Tri";
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
        if (containedIngredients.Count == 2)
        {
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
