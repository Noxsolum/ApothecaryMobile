using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    public GameObject refiner;
    string ingredientName;
    public Sprite spr_ingredient;
    public Sprite pick_ingredient;
    public string newName = "";
    public bool refineTimer = false;
    public int ID;
    float fiveSec = 5;
    public BackgroundScript backgroundScript;

    private void Start()
    {
        backgroundScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BackgroundScript>();
        refiner = null;

        if (spr_ingredient == null)
            spr_ingredient = GetComponent<SpriteRenderer>().sprite;
    }

    public void RefineResource(string ing)
    {
        ingredientName = ing;
        this.name = ingredientName;
        this.GetComponent<SpriteRenderer>().sprite = spr_ingredient;
        this.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<BoxCollider2D>().size = new Vector2(3.64f, 3.28f);
        Debug.Log( "Successful changed to " + ingredientName);
    }

    public void pickResource()
    {
        this.GetComponent<SpriteRenderer>().sprite = pick_ingredient;
        this.name = this.name + "Picked";
    }

    public string returnName()
    {
        return ingredientName;
    }

    public void Update()
    {
        if (refineTimer == true && !backgroundScript.isPaused)
        {
            Debug.Log("StartTimer");
            fiveSec -= Time.deltaTime;
            if (fiveSec <= 0)
            {
                Debug.Log("EndTimer");
                fiveSec = 5;
                refineTimer = false;
                RefineResource(newName);
            }
        }
    }
}
