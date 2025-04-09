using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickingAndPlacing : MonoBehaviour
{
    private GameObject _isCarrying = null;
    private GameObject _playerOne;
    private GameObject[] _potato;
    public Vector3 refinePos;
    public Vector2 moveDir;
    public float fiveSec = 5;
    public int herbNum = 0;
    Animator Carrying;



    public bool startTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        _isCarrying = null;
        _playerOne = this.gameObject;
        _potato = GameObject.FindGameObjectsWithTag("Carryable");
        Carrying = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {

            GameObject seenItem = null;
            if (_isCarrying == null)
            {
                seenItem = checkPickUp("Carryable");
                if(seenItem != null)
                {
                    _isCarrying = seenItem;
                    seenItem.transform.SetParent(this.transform);
                    seenItem.GetComponent<BoxCollider2D>().enabled = false;
                    seenItem.transform.localPosition = new Vector2(0f, 0.8f);
                    Carrying.SetBool("carrying", true);

                    if(seenItem.CompareTag("Herb"))
                    {
                        seenItem.GetComponent<ResourceScript>().pickResource();
                    }
                }
            }
            else if (_isCarrying != null) // When you're carrying an item
            {
                seenItem = checkPickUp("Droppable");
                if(seenItem != null)
                {
                    if (seenItem.name.Contains("Cauldron") && _isCarrying.CompareTag("Ingredient"))
                    {
                        Debug.Log(checkIngredients(seenItem));
                        if (checkIngredients(seenItem) == true)
                        {
                            seenItem.GetComponent<mixingPotScript>().inputItem(_isCarrying);
                            seenItem.GetComponent<SpriteRenderer>().enabled = true;
                            Carrying.SetBool("carrying", false);
                            Destroy(_isCarrying);
                        }
                    }
                    else if (seenItem.name.Contains("Refiner") && _isCarrying.CompareTag("Herb"))
                    {
                        _isCarrying.transform.SetParent(seenItem.transform);
                        _isCarrying.tag = "Ingredient";
                        _isCarrying.transform.localPosition = new Vector2(0f, 0f);
                        _isCarrying.GetComponent<ResourceScript>().startTimer = true;
                        _isCarrying.GetComponent<ResourceScript>().newName = "Refined " + _isCarrying.name;
                        Carrying.SetBool("carrying", false);
                        _isCarrying = null;
                    }
                    else if (seenItem.name.Contains("Customer") && _isCarrying.CompareTag("Potion"))
                    {
                        Carrying.SetBool("carrying", false);
                        Destroy(_isCarrying);
                    }
                    else if (seenItem.name.Contains("Bin"))
                    {
                        Carrying.SetBool("carrying", false);
                        Debug.Log("Item DESTROYED");
                        Destroy(_isCarrying);
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

    bool checkIngredients(GameObject cauldron)
    {
        if (cauldron.GetComponent<mixingPotScript>().returnListSize() < 2)
        {
            return true;
        }
        return false;
    }
}
