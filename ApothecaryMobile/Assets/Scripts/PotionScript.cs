using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public List<GameObject> ar_Potions;
    public List<GameObject> ar_resources;



    // Start is called before the first frame update
    void Awake()
    {
        

    }

    public int ReturnID(GameObject lookingForID)
    {
        foreach (GameObject i in ar_Potions)
        {
            if (i.name == lookingForID.name)
            {
                Debug.LogError("Hit1");
                return i.GetComponent<ResourceScript>().ID;
            }
        }
        foreach (GameObject i in ar_resources)
        {
            if (i.name == lookingForID.name)
            {
                Debug.LogError("Hit2");
                return i.GetComponent<ResourceScript>().ID;
            }
        }
        return 0;
    }

    public GameObject ReturnObj(int lookingForID)
    {
        foreach(GameObject i in ar_Potions)
        {
            if (i.GetComponent<ResourceScript>().ID == lookingForID)
                return i;
        }
        foreach (GameObject i in ar_resources)
        {
            if (i.GetComponent<ResourceScript>().ID == lookingForID)
                return i;
        }
        return null;
    }

    void Update()
    {
        
    }
}
