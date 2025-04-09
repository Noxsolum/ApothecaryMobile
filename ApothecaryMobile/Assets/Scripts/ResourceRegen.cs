using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRegen : MonoBehaviour
{
    private GameObject Mushroom, Berries, Radish, Sage, Lavinder, Snowdrop;
    public GameObject BackgroundScript;
    public string herbType = null;
    public bool growTimer = false;
    public bool spawn1, spawn2, spawn3, spawn4, spawn5, spawn6 = false;
    private float fiveSec = 5;
    private string _herb = null;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundScript = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(!BackgroundScript.GetComponent<BackgroundScript>().isPaused)
        {
            Mushroom = GameObject.Find("Mushroom");
            Berries = GameObject.Find("Berries");
            Radish = GameObject.Find("Radish");
            Sage = GameObject.Find("Sage");
            Lavinder = GameObject.Find("Lavinder");
            Snowdrop = GameObject.Find("Snowdrop");
            if (Mushroom == null)
            {
                growTimer = true;
                if (spawn1 == true)
                {
                    spawn1 = false;
                    GrowPlant("Mushroom");
                }
            }

            if (Berries == null)
            {
                growTimer = true;
                if (spawn2 == true)
                {
                    spawn2 = false;
                    GrowPlant("Berries");
                }
            }

            if (Radish == null)
            {
                growTimer = true;
                if (spawn3 == true)
                {
                    spawn3 = false;
                    GrowPlant("Radish");
                }
            }

            if (Sage == null)
            {
                growTimer = true;
                if (spawn4 == true)
                {
                    spawn4 = false;
                    GrowPlant("Sage");
                }
            }

            if (Lavinder == null)
            {
                growTimer = true;
                if (spawn5 == true)
                {
                    spawn5 = false;
                    GrowPlant("Lavinder");
                }
            }

            if (Snowdrop == null)
            {
                growTimer = true;
                if (spawn6 == true)
                {
                    spawn6 = false;
                    GrowPlant("Snowdrop");
                }
            }

            if (growTimer == true)
            {
                Debug.Log("StartTimer");
                fiveSec -= Time.deltaTime;
                if (fiveSec <= 0)
                {
                    _herb = herbType;
                    Debug.Log("EndTimer");
                    fiveSec = 5;
                    growTimer = false;
                    if (Mushroom == null) { spawn1 = true; }
                    if (Berries == null) { spawn2 = true; }
                    if (Radish == null) { spawn3 = true; }
                    if (Sage == null) { spawn4 = true; }
                    if (Lavinder == null) { spawn5 = true; }
                    if (Snowdrop == null) { spawn6 = true; }
                }
            }
        }
    }

    private void GrowPlant(string herby)
    {
        switch (herby)
        {
            case "Mushroom":
                Instantiate(Resources.Load("Prefabs/" + herby), new Vector3(2.08f,0.345f,0f), new Quaternion(0, 0, 0, 0));
                Mushroom = GameObject.Find("Mushroom(Clone)");
                Mushroom.name = "Mushroom";
                break;
            case "Berries":
                Instantiate(Resources.Load("Prefabs/" + herby), new Vector3(0f, 0.325f, 0f), new Quaternion(0, 0, 0, 0));
                Berries = GameObject.Find("Berries(Clone)");
                Berries.name = "Berries";
                break;
            case "Radish":
                Instantiate(Resources.Load("Prefabs/" + herby), new Vector3(-2f, 1.0f, 0f), new Quaternion(0, 0, 0, 0));
                Radish = GameObject.Find("Radish(Clone)");
                Radish.name = "Radish";
                break;
            case "Sage":
                Instantiate(Resources.Load("Prefabs/" + herby), new Vector3(2.115f, -1.045f, 0f), new Quaternion(0, 0, 0, 0));
                Sage = GameObject.Find("Sage(Clone)");
                Sage.name = "Sage";
                break;
            case "Lavinder":
                Instantiate(Resources.Load("Prefabs/" + herby), new Vector3(0.2f, -1.045f, 0f), new Quaternion(0, 0, 0, 0));
                Lavinder = GameObject.Find("Lavinder(Clone)");
                Lavinder.name = "Lavinder";
                break;
            case "Snowdrop":
                Instantiate(Resources.Load("Prefabs/" + herby), new Vector3(-1.94f, -1.015f, 0f), new Quaternion(0, 0, 0, 0));
                Snowdrop = GameObject.Find("Snowdrop(Clone)");
                Snowdrop.name = "Snowdrop";
                break;
            default:
                break;
        }
    }
}
