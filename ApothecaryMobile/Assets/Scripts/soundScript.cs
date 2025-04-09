using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundScript : MonoBehaviour
{
    public AudioClip[] soundClips;
    //public AudioSource toPlay;
    // Start is called before the first frame update

    public List<AudioSource> Sources;
    void Start()
    {
        for (int i = 0; i < this.GetComponents<AudioSource>().Length; i++)
        {
            Sources.Add(this.GetComponents<AudioSource>()[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void soundArray(int trackNumber, int ASource)
    {
        if (!Sources[ASource].isPlaying)
        {
            AudioSource sound = Sources[ASource];
            sound.clip = soundClips[trackNumber];
            Sources[ASource].Play();
        }
    }

    public void stop(int aSource)
    {
        AudioSource sound = Sources[aSource];
        //sound.clip = soundClips[trackNumber];
        GetComponent<AudioSource>().Stop();
    }
}
