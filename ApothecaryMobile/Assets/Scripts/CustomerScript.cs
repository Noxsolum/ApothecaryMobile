using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerScript : MonoBehaviour
{
    public List<string> requestedItem;
    
    public string Request;
    public float waitTime = 20f;

    public GameObject[] customerTextboxes = new GameObject[3];
    private Text[] customerText = new Text[3];

    public List<CustomerStorage> list_CustStor;

    PotionScript PtScr;

    // Start is called before the first frame update
    void Start()
    {
        PtScr = GameObject.Find("Main Camera").GetComponent<PotionScript>();

        foreach(Transform i in this.transform)
            list_CustStor.Add(i.gameObject.GetComponent<CustomerStorage>());

        for (int i = 0; i < 3; i++)
        {
            customerText[i] = customerTextboxes[i].GetComponent<Text>();
        }
        

        if (requestedItem.Count == 0)
            Invoke("NewCustomer", 1f);

        InvokeRepeating("NewCustomer", waitTime + 1f, waitTime);
    }

    void NewCustomer()
    {
        bool OrderPlaced = false;
        if (requestedItem.Count < 3)
        {
            foreach (Text txtBox in customerText)
            {
                if ((txtBox.text == "" || txtBox.text == "Default") && !OrderPlaced)
                {
                    Request = returnRandomItem(Random.Range(0, PtScr.ar_Potions.Count)).name;
                    requestedItem.Add(Request);
                    customerText[requestedItem.LastIndexOf(Request)].text = Request;
                    list_CustStor[requestedItem.LastIndexOf(Request)].AddDesiredItem(Request, txtBox);
                    Debug.Log("New Random Item: " + Request + " for " + customerText[requestedItem.LastIndexOf(Request)]);
                    OrderPlaced = true;
                }
            }
        }

    }

    GameObject returnRandomItem(int rand)
    {
        return PtScr.ar_Potions[rand];
    }
    

}
