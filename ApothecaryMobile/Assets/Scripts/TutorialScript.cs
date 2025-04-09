using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] public GameObject[] tutorialBoxes;
    public GameObject ContinueTap;
    bool NextBoxActive;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject box in tutorialBoxes)
        {
            box.SetActive(false);
        }
        tutorialBoxes[0].SetActive(true);

        NextBoxActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TapToContinue()
    {
        if(ContinueTap.activeInHierarchy == true)
        {
            foreach (GameObject box in tutorialBoxes)
            {
                if (box.activeInHierarchy == true)
                {
                    if(box.name.Contains("Final"))
                    {
                        SceneManager.LoadScene(0);
                    }
                    box.SetActive(false);
                    NextBoxActive = true;
                    continue;
                }

                if (NextBoxActive)
                {
                    box.SetActive(true);
                    NextBoxActive = false;
                    ContinueBox();
                }
            }
        }
    }

    void ContinueBox()
    {
        ContinueTap.SetActive(false);
        ThreeSecDelay();
        ContinueTap.SetActive(true);
    }

    IEnumerator ThreeSecDelay()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
