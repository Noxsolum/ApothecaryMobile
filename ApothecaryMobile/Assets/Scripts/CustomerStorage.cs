using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerStorage : MonoBehaviour
{

    private string desiredItem;
    private Text DesignatedTxt;


    public void AddDesiredItem(string desItem, Text txtbox)
    {
        desiredItem = desItem;
        DesignatedTxt = txtbox;
    }

    public bool CheckIfSameItem(string item)
    {
        if (item.Contains(desiredItem))
            return true;

        return false;
    }

    public void removeDesiredItem()
    {
        desiredItem = "";
        DesignatedTxt.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
