using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundScript : MonoBehaviour
{

    public float masterClock = 0;

    public GameObject obj_Slider;

    public List<GameObject> PausedObjs;
    public GameObject WinMenu;

    private float additionalTime;
    private Slider sld_timer;

    public GameObject win_txtbox;
    private Text txt_win;
    private int points;

    public bool isPaused;

    public GameObject Bin;
    public GameObject Sign;
    public GameObject player;
    public GameObject EndGame;
    public GameObject Cauldron;

    private soundScript signSounds;

    private GameObject recipeBooks;

    private void Awake()
    {
        recipeBooks = GameObject.FindGameObjectWithTag("Recipes");
        player = GameObject.FindGameObjectWithTag("Player");
        Bin = GameObject.FindGameObjectWithTag("Bin");
        Cauldron = GameObject.FindGameObjectWithTag("Cauldron");
        recipeBooks.SetActive(false);
    }

    private void Start()
    {
        signSounds = GameObject.FindWithTag("Audio").GetComponent<soundScript>();
        txt_win = win_txtbox.GetComponent<Text>();
        isPaused = false;
        if (obj_Slider == null)
            obj_Slider = GameObject.Find("Slider");

        sld_timer = obj_Slider.GetComponent<Slider>();
        Invoke("TurnSignOff", 2f);
    }

    public void PauseAnimation()
    {
        if(isPaused)
        {
            player.GetComponent<AnimationScript>().PauseAnimation(0);
            Bin.GetComponent<AnimationScript>().PauseAnimation(0);
            Cauldron.GetComponent<AnimationScript>().PauseAnimation(0);
        }
        else
        {
            player.GetComponent<AnimationScript>().PauseAnimation(1);
            Bin.GetComponent<AnimationScript>().PauseAnimation(1);
            Cauldron.GetComponent<AnimationScript>().PauseAnimation(1);
        }
    }

    void TurnSignOff()
    {
        Sign.SetActive(false);

    }

    public void AddTime(float time)
    {
        additionalTime += time;
    }

    void GetAllActiveObj()
    {
        PausedObjs.Clear();
        PausedObjs.AddRange(UnityEngine.Object.FindObjectsOfType<GameObject>());
        foreach (GameObject i in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            if (!i.activeInHierarchy || i.name == "Main Camera" || i.layer == 5)
                PausedObjs.Remove(i);
        }
    }


    void Update()
    {
        if(masterClock >= sld_timer.maxValue)
        {
            WinMenu.SetActive(true);
            setObjActive(false);
            Sign.SetActive(true);
            EndGame.SetActive(true);
            Sign.GetComponent<Animator>().SetBool("isOpen", false);
            txt_win.text = "You got $" + points + "!";
            //signSounds.soundArray(9, 1);
        }

        if (!isPaused)
        {
            masterClock += Time.deltaTime + additionalTime;
            updateTimeSlider();
        }
    }

    public void AddToPoints(int changeAmount)
    {
        points += changeAmount;
    }

    public void setObjActive(bool activate)
    {
        isPaused = !activate;
        //if (!activate)
        //    GetAllActiveObj();

        //EndGame.SetActive(!activate);

        //foreach (GameObject i in PausedObjs)
        //{
        //    i.SetActive(activate);
        //}
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    void updateTimeSlider()
    {
        sld_timer.value = sld_timer.maxValue - masterClock;
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
