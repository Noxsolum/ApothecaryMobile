using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private float minutes;
    public GameObject backgroundScript;
    static float timer = 120;
    float currTime = timer;

    // Start is called before the first frame update
    void Start()
    {
        minutes = 360 / timer;
        backgroundScript = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float time = 120 - Time.deltaTime;

        if (!backgroundScript.GetComponent<BackgroundScript>().isPaused && time >= 0)
            transform.Rotate(-Vector3.forward * (Time.deltaTime * minutes));
    }
}
