using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingAndPlacing : MonoBehaviour
{
    private bool isCarrying = false;


    // Start is called before the first frame update
    void Start()
    {
        isCarrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if (isCarrying == false)
            {
                if (/* Check for object that is pick up-able */ )
                {
                    isCarrying = true;
                }
            }
            else
            {
                if(/* Check is the thing is put down-able */)
                {

                }
            }

        }
    }
}
