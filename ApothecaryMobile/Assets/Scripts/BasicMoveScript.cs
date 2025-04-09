using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class BasicMoveScript : MonoBehaviour
{
    public BackgroundScript backgroundScript;
    private GameObject _playerOne;
    private Rigidbody2D player_RB;
    private Animator _animator;

    public float speed = 5;
    //public float dir = 0; // 0 = Down, 1 = Up, 2 = Left, 3 = Right
    public Vector2 dir = new Vector2(0, 0);
    private soundScript movementSound;
    bool isMoving;
    // Start is called before the first frame update

    void Start()
    {
        _playerOne = this.gameObject;
        player_RB = _playerOne.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        movementSound = GameObject.FindWithTag("Audio").GetComponent<soundScript>();
        backgroundScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BackgroundScript>();
    }

    void Update()
    {
        PickingAndPlacing PnP = GetComponent<PickingAndPlacing>();
        dir = new Vector2(0, 0);

        //if (Application.platform == RuntimePlatform.WindowsEditor)
        //    HandleKeys(player_RB, PnP);
        //else if (Application.platform == RuntimePlatform.Android)
        if(!backgroundScript.isPaused)
        {
            HandleTouch(player_RB, PnP);
            player_RB.AddForce(dir * speed);
        }

        HandleSound();
    }

    /// <summary>
    /// 
    /// </summary>
    void HandleSound()
    {
        if (player_RB.linearVelocity.magnitude > 0.5)
        {
            // (Sound Clip Number in Array, Audio Source in order number)
            movementSound.soundArray(4, 0);
            _animator.SetBool("isRunning", true);
        }
        else
        {
            movementSound.stop(0);
            _animator.SetBool("isRunning", false);
        }

        if (!Input.anyKey || player_RB.linearVelocity.magnitude == 0)
            movementSound.stop(0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="obj"></param>
    void HandleKeys(Rigidbody2D player, PickingAndPlacing obj)
    {
        if (Input.GetKey(KeyCode.W))
        {
            dir += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += new Vector2(0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += new Vector2(1, 0);
        }

        if (dir != new Vector2(0, 0))
            obj.moveDir = dir;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="obj"></param>
    void HandleTouch(Rigidbody2D player, PickingAndPlacing obj)
    {
        Vector2 playerTouch;
        if(Input.touchCount == 0)
        {
            dir = new Vector2(0, 0);
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 1)
        {
            playerTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            if (Input.GetTouch(0).phase != TouchPhase.Ended || Input.GetTouch(0).phase != TouchPhase.Canceled)
            {
                if (playerTouch.y > this.transform.position.y)
                {
                    dir += new Vector2(0, 1);
                }

                if (playerTouch.x < this.transform.position.x)
                {
                    dir += new Vector2(-1, 0);
                }

                if (playerTouch.y < this.transform.position.y)
                {
                    dir += new Vector2(0, -1);
                }

                if (playerTouch.x > this.transform.position.x)
                {
                    dir += new Vector2(1, 0);
                }
            }
        }

        Debug.Log(dir);

        if (dir != new Vector2(0, 0))
            obj.moveDir = dir;
    }
}