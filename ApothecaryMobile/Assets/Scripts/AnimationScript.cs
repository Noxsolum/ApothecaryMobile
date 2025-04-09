using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseAnimation(int pause)
    {
        if(this.GetComponent<Animator>().speed != pause)
            this.GetComponent<Animator>().speed = pause;
    }
}
