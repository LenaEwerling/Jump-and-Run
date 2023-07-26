using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float leftBound = -15;
    private float counterBound = 2;
    private bool alreadyScored = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < counterBound && !alreadyScored)
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
            Debug.Log(PlayerPrefs.GetInt("Score"));
            alreadyScored = true;
        }
    }
}
