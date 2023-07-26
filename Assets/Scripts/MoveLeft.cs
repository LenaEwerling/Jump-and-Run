using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 10;
    public bool hasToMoveBack;
    private PlayerController playerControllerScript;
    private int boostLength = 3;
    private float normalSpeed;
    //private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Started") == 0)
        {
            if (playerControllerScript.GetComponent<Rigidbody>().position.x >= 6)
            {
                Debug.Log("Start");
                PlayerPrefs.SetInt("Started", 1);
            }
        }
        if (PlayerPrefs.GetInt("Started") == 1)
        {
            ControllSpeed();
            Movement();
            Boost();
        }
    }

    void Movement()
    {
        if (! playerControllerScript.gameOver)
        {
            if (hasToMoveBack)
            {

                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
    }

    void ControllSpeed()
    {
        if (PlayerPrefs.GetInt("Boost") == 0)
        {
            if (PlayerPrefs.GetInt("Score") < 20)
            {
                speed = 10;
                normalSpeed = speed;
            }
            else if (PlayerPrefs.GetInt("Score") < 50)
            {
                speed = 12;
                normalSpeed = speed;
            }
            else if (PlayerPrefs.GetInt("Score") < 100)
            {
                speed = 14;
                normalSpeed = speed;
            }
            else
            {
                speed = 16;
            }
        }
        
    }

    void Boost()
    {
        if (PlayerPrefs.GetInt("Boost") != 0)
        {
            boostLength = PlayerPrefs.GetInt("Score") - PlayerPrefs.GetInt("Boost");
            //Debug.Log(boostLength);
            if (boostLength == 3)
            {
                PlayerPrefs.SetInt("Boost", 0);
                speed = normalSpeed;
            }
            else
            {
                speed = normalSpeed * 1.5f;
                if (gameObject.CompareTag("Obstacle"))
                {
                    speed = speed + 10;
                }
            }
            //Debug.Log("Speed: " + speed);
        }
    }
}
