using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickingAndPlacing : MonoBehaviour
{
    private GameObject _isCarrying = null;
    private GameObject _playerOne;
    private GameObject _herbPatch;
    private ResourceRegen _regen;
    public Vector3 refinePos;
    public Vector2 moveDir;
    public float fiveSec = 5;
    public int herbNum = 0;
    Animator Carrying;
    private soundScript soundCaller;
    public BackgroundScript backgroundScript;

    public bool startTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        _isCarrying = null;
        _playerOne = this.gameObject;
        _herbPatch = GameObject.FindGameObjectWithTag("GameController");
        _regen = _herbPatch.GetComponent<ResourceRegen>();
        Carrying = GetComponent<Animator>();
        soundCaller = GameObject.FindWithTag("Audio").GetComponent<soundScript>();
        backgroundScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BackgroundScript>();
    }


    void Update()
    {
        if(!backgroundScript.isPaused)
        {
            //if (Application.platform == RuntimePlatform.WindowsEditor)
            //    keyboardInput();
            //else if (Application.platform == RuntimePlatform.Android)
            touchInput();
        }
    }

    void keyboardInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject seenItem = null;
            if (_isCarrying == null)
            {
                seenItem = checkPickUp("Carryable");
                if (seenItem != null)
                {
                    _isCarrying = seenItem;
                    seenItem.transform.SetParent(this.transform);
                    seenItem.GetComponent<BoxCollider2D>().enabled = false;
                    seenItem.transform.localPosition = new Vector2(0f, 1.0f);
                    Carrying.SetBool("carrying", true);

                    if (seenItem.CompareTag("Herb"))
                    {
                        seenItem.GetComponent<ResourceScript>().pickResource();
                        _regen.herbType = _isCarrying.name;
                        _regen.growTimer = true;
                        soundCaller.soundArray(2, 1);
                    }
                    if (seenItem.CompareTag("Ingredient"))
                        soundCaller.soundArray(7, 1);
                    if (seenItem.CompareTag("Potion"))
                        soundCaller.soundArray(8, 1);
                }
            }
            else if (_isCarrying != null) // When you're carrying an item
            {
                seenItem = checkPickUp("Droppable");
                if (seenItem != null)
                {
                    if (seenItem.name.Contains("Cauldron") && _isCarrying.CompareTag("Ingredient") && _isCarrying.GetComponent<ResourceScript>().ID != seenItem.GetComponent<mixingPotScript>().returnFirstIngre())
                    {
                        seenItem.GetComponent<mixingPotScript>().inputItem(_isCarrying.GetComponent<ResourceScript>().ID);
                        seenItem.GetComponent<SpriteRenderer>().enabled = true;
                        Carrying.SetBool("carrying", false);
                        soundCaller.soundArray(3, 1);
                        Destroy(_isCarrying);
                        if (checkIngredients(seenItem) == true)
                        {
                            seenItem.GetComponent<mixingPotScript>().brewTimer = true;
                            seenItem.GetComponent<Animator>().SetTrigger("NewAnim");
                        }
                    }
                    else if (seenItem.name.Contains("Refiner") && _isCarrying.CompareTag("Herb"))
                    {
                        _isCarrying.transform.SetParent(seenItem.transform);
                        _isCarrying.tag = "Ingredient";
                        _isCarrying.transform.localPosition = new Vector2(0f, 0f);
                        _isCarrying.GetComponent<ResourceScript>().refineTimer = true;
                        _isCarrying.GetComponent<ResourceScript>().newName = "Refined " + _isCarrying.name;
                        Carrying.SetBool("carrying", false);
                        _isCarrying = null;
                    }
                    else if (seenItem.name.Contains("Customer") && _isCarrying.CompareTag("Potion"))
                    {

                        if (seenItem.GetComponent<CustomerStorage>().CheckIfSameItem(_isCarrying.name))
                        {
                            GameObject.Find("Main Camera").GetComponent<BackgroundScript>().AddToPoints(10);
                            Debug.Log("Sold!");
                            Carrying.SetBool("carrying", false);
                            Destroy(_isCarrying);
                        }
                        else
                        {
                            Debug.Log("You're a terrible Shopkeeper!");
                        }
                        soundCaller.soundArray(6, 1);


                    }
                    else if (seenItem.name.Contains("Bin"))
                    {
                        soundCaller.soundArray(1, 1);
                        Carrying.SetBool("carrying", false);
                        Debug.Log("Item DESTROYED");
                        Destroy(_isCarrying); //
                    }
                    else
                    {
                        Debug.Log("Can't Put Down");
                    }
                }
            }
        }
    }

    void touchInput()
    {
        GameObject seenItem = null;
        if (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 2)
        {
            Vector2 raycast = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 dir = -((Vector2)this.transform.position - raycast).normalized;
            RaycastHit2D rayCastHit = Physics2D.Raycast(this.transform.position, dir);
            seenItem = rayCastHit.collider.gameObject;

            if (seenItem != null)
            {
                Debug.Log("Hit an Object");

                if (_isCarrying == null)
                {
                    seenItem = checkPickUp("Carryable", dir);

                    _isCarrying = seenItem;
                    seenItem.transform.SetParent(this.transform);
                    seenItem.GetComponent<BoxCollider2D>().enabled = false;
                    seenItem.transform.localPosition = new Vector2(0f, 1.0f);
                    Carrying.SetBool("carrying", true);

                    if (seenItem.CompareTag("Herb"))
                    {
                        seenItem.GetComponent<ResourceScript>().pickResource();
                        _regen.herbType = _isCarrying.name;
                        _regen.growTimer = true;
                        soundCaller.soundArray(2, 1);
                    }
                    else if (seenItem.CompareTag("Ingredient"))
                    {
                        soundCaller.soundArray(7, 1);
                        seenItem.GetComponent<ResourceScript>().refiner.GetComponent<StorageScript>().hasItem = false;
                    }
                    else if (seenItem.CompareTag("Potion"))
                    {
                        soundCaller.soundArray(8, 1);
                    }
                }
                else if (_isCarrying != null) // When you're carrying an item
                {
                    seenItem = checkPickUp("Droppable", dir);

                    if (seenItem.name.Contains("Cauldron") && _isCarrying.CompareTag("Ingredient") && _isCarrying.GetComponent<ResourceScript>().ID != seenItem.GetComponent<mixingPotScript>().returnFirstIngre())
                    {
                        seenItem.GetComponent<mixingPotScript>().inputItem(_isCarrying.GetComponent<ResourceScript>().ID);
                        seenItem.GetComponent<SpriteRenderer>().enabled = true;
                        Carrying.SetBool("carrying", false);
                        soundCaller.soundArray(3, 1);
                        Destroy(_isCarrying);
                        if (checkIngredients(seenItem) == true)
                        {
                            seenItem.GetComponent<mixingPotScript>().brewTimer = true;
                            seenItem.GetComponent<Animator>().SetTrigger("NewAnim");
                        }
                    }
                    else if (seenItem.name.Contains("Refiner") && _isCarrying.CompareTag("Herb"))
                    {
                        if (seenItem.gameObject.GetComponent<StorageScript>().hasItem == false)
                        {
                            _isCarrying.transform.SetParent(seenItem.transform);
                            _isCarrying.tag = "Ingredient";
                            _isCarrying.transform.localPosition = new Vector2(0f, 0f);
                            _isCarrying.GetComponent<ResourceScript>().refineTimer = true;
                            _isCarrying.GetComponent<ResourceScript>().newName = "Refined " + _isCarrying.name;
                            _isCarrying.GetComponent<ResourceScript>().refiner = seenItem.gameObject;
                            Carrying.SetBool("carrying", false);
                            _isCarrying = null;
                            seenItem.gameObject.GetComponent<StorageScript>().hasItem = true;
                        }
                        else { Debug.Log("Refiner is full!!"); }

                    }
                    else if (seenItem.name.Contains("Customer") && _isCarrying.CompareTag("Potion"))
                    {
                        if (seenItem.GetComponent<CustomerStorage>().CheckIfSameItem(_isCarrying.name))
                        {
                            GameObject.Find("Main Camera").GetComponent<BackgroundScript>().AddToPoints(10);
                            Debug.Log("Sold!");
                            Carrying.SetBool("carrying", false);
                            Destroy(_isCarrying);
                        }
                        else
                        {
                            Debug.Log("You're a terrible Shopkeeper!");
                        }
                        soundCaller.soundArray(6, 1);


                    }
                    else if (seenItem.name.Contains("Bin"))
                    {
                        soundCaller.soundArray(1, 1);
                        Carrying.SetBool("carrying", false);
                        Debug.Log("Item DESTROYED");
                        Destroy(_isCarrying); //
                    }
                    else
                    {
                        Debug.Log("Can't Put Down");
                    }
                }
            }
        }
    }

    GameObject checkPickUp(string maskName)
    {
        LayerMask mask = LayerMask.GetMask(maskName);
        if (moveDir != new Vector2(0, 0))
        {
            RaycastHit2D rCast = Physics2D.Raycast(_playerOne.transform.position, moveDir, 3f, mask);
            Debug.DrawRay(transform.position, moveDir * 2, Color.green, 5f);

            if (rCast.collider != null)
            {
                return rCast.collider.gameObject;
            }
            else { return null; }
        }
        return null;
    }
    GameObject checkPickUp(string maskName, Vector2 dir)
    {
        LayerMask mask = LayerMask.GetMask(maskName);
        if (moveDir != new Vector2(0, 0))
        {
            RaycastHit2D rCast = Physics2D.Raycast(_playerOne.transform.position, dir, 3f, mask);
            Debug.DrawRay(transform.position, dir * 2, Color.green, 5f);

            if (rCast.collider != null)
            {
                return rCast.collider.gameObject;
            }
            else { return null; }
        }
        return null;
    }

    bool checkIngredients(GameObject cauldron)
    {
        if (cauldron.GetComponent<mixingPotScript>().returnListSize() == 2)
        {
            return true;
        }
        return false;
    }
}
