using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private float startDelay = 2;
    private PlayerController playerControllerScript;
    private int counter = 0;
    private float minimalRepeatTime = 4; 
    private float maximalRepeatTime = 6;
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Invoke()
    {
        InvokeRepeating("SpawnRandomObstacle", startDelay, Random.Range(minimalRepeatTime, maximalRepeatTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Started") == 1 && !started)
        {
            Invoke();
            started = true;
        }
        if (counter == 5)
        {
            counter = 0;
            if (minimalRepeatTime >= 0.75f)
            {
                minimalRepeatTime -= 0.75f;
            }
            if (maximalRepeatTime >= 0.75f)
            {
                maximalRepeatTime -= 0.75f;
            }
            //Debug.Log(minimalRepeatTime); 
        }
    }

    private void SpawnRandomObstacle()
    {
        if (! playerControllerScript.gameOver)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Vector3 spawnPos;
            if (obstacleIndex == 1)
            {
                spawnPos = new Vector3(30, 0.5f, 0);
            }
            else
            {
                spawnPos = new Vector3(30, 0, 0);
            }
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, 
            obstaclePrefabs[obstacleIndex].transform.rotation);
            counter++;
        }
    }
}
