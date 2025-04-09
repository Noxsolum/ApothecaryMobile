using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : MonoBehaviour
{
    public string storedItem;
    public int amountOfItem;
    public bool hasItem;
    
    public string takeItem()
    {
        if (amountOfItem <= 0)
            return null;
        else
            return storedItem;
    }

    private void Start()
    {
        this.hasItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
