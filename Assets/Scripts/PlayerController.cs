using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    private bool isOnGround = true;
    public bool gameOver = false;
    private bool isReady = false;
    private int doubleJumpCounter = 0;
    private int lastSpeedBoost = 0;

    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAnim.SetInteger("DeathType_int", 1);
        playerAudio = GetComponent<AudioSource>();
        //Debug.Log(playerAudio);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Boost", 0);
        PlayerPrefs.SetInt("Start", 0);
        playerRb.position = new Vector3(2, 0, 0);
        playerAnim.SetFloat("Speed_f", 0.51f);
        PlayerPrefs.SetInt("Started", 0);
        dirtParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRb.position.x < 6)
        {
            playerRb.position = new Vector3(playerRb.position.x + 0.005f, 0, 0);
        }
        if (playerRb.position.x >= 6 && !isReady)
        {
            Debug.Log("GetReady");
            GetReady();
        }
        if (isReady)
        {
            Jumping();
            SpeedBoost();
        }
    }

    void GetReady()
    {
        playerAnim.SetFloat("Speed_f", 0.7f);
        isReady = true;
        dirtParticle.Play();
    }

    void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (isOnGround || doubleJumpCounter < 2) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetFloat("Speed_f", 0.1f);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJumpCounter ++;
        }
    }

    void SpeedBoost()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerPrefs.GetInt("Score") - lastSpeedBoost >= 2)
        {
            Debug.Log("SpeedBoost!");
            PlayerPrefs.SetInt("Boost", PlayerPrefs.GetInt("Score"));
            lastSpeedBoost = PlayerPrefs.GetInt("Score");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!gameOver)
            {
                gameOver = true;
                Debug.Log("GameOver!");
                playerAnim.SetFloat("Speed_f", 0.0f);
                playerAnim.SetBool("Death_b", true);
                explosionParticle.Play();
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSound, 1.0f);
                Debug.Log(PlayerPrefs.GetInt("Score"));
                PlayerPrefs.SetInt("Score", 0);
                PlayerPrefs.SetInt("Boost", 0);
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            //Debug.Log("Ground");
            if (isReady)
            {
                if (PlayerPrefs.GetInt("Score") < 20)
                {
                    playerAnim.SetFloat("Speed_f", 0.7f);
                }
                else if (PlayerPrefs.GetInt("Score") < 50)
                {
                    playerAnim.SetFloat("Speed_f", 0.8f);
                }
                else if (PlayerPrefs.GetInt("Score") < 100)
                {
                    playerAnim.SetFloat("Speed_f", 0.9f);
                }
                else
                {
                    playerAnim.SetFloat("Speed_f", 1.0f);
                }
                dirtParticle.Play();
                doubleJumpCounter = 0;
            }
        }  
    }
}
