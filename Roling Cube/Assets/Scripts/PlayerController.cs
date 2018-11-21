using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Text Trap;

    private Rigidbody rb;
    private float distToGround;
    public bool GameOver; 
    private bool GameWon;

    public Text ScoreText;
    public Text FinishText;
    public Text TimerText;
    public Text GameOverText;

    public float speed;
    public float jumpSpeed;
    public int score;
    public float Timer;
    public float movementType;
    public int minPoints;
    public float boost;


    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        score = 0;
        ScoreText.text = "Score: " + score.ToString();
        GameOver = false;
        GameWon = false;

        distToGround = this.GetComponent<BoxCollider>().bounds.extents.y;
    }


    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
            print ("Quit requested");
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (GameOver == false && GameWon == false)
        {
            Countdown();
        }

        if (GameWon == true)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

        }

        if (Timer <= 0)
        {
            GameOver = true;
            GameOverText.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
           switchMovementType();
        }
    }


    //Everything that has to do with physics, should be in here
    void FixedUpdate()
    {

        if (GameOver == false && GameWon == false)
        {

            if (movementType == 1)
            {
                Movement1();
            } else if (movementType == 2)
            {
            Movement2();
            }

            Jumping();
        }
    }


    //-----------------------------------------------------------------------------------
    //Movement Functions
    //-----------------------------------------------------------------------------------

    //Using rigidbody force
    void Movement1()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    //Using transform.positon
    void Movement2()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); 
        float moveVertical = Input.GetAxis("Vertical");  

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * Time.deltaTime * speed;
        this.transform.position = movement + this.transform.position;
    }


    //-----------------------------------------------------------------------------------
    //Collision Checks
    //-----------------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            score += 1;
            ScoreText.text = "Score: " + score.ToString();
            Destroy(other.gameObject);
        }


        if (other.gameObject.CompareTag("Death"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameOver = true;
            GameOverText.gameObject.SetActive(true);
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            GameOver = true;
            Trap.gameObject.SetActive(true);
        }

        if (other.gameObject.CompareTag("Finish") && score >= minPoints && GameOver == false)
        {
            FinishText.gameObject.SetActive(true);
            GameWon = true;
        }
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Vector3 movement = new Vector3(-100.0f, 0.0f, 0.0f);
            rb.AddForce(movement * boost);
        }
    }


    //-----------------------------------------------------------------------------------
    //Countdown function
    //-----------------------------------------------------------------------------------
    void Countdown()
    {

        Timer -= Time.deltaTime;

        TimerText.text = "Time Left: " + Timer.ToString("0");

    }


    //-----------------------------------------------------------------------------------
    //Jumping Functions
    //-----------------------------------------------------------------------------------
    void Jumping()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
         return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    //Checks for button press
    public void switchMovementType()
    {
        if (movementType == 1)
        {
            movementType = 2;
        } else if (movementType ==2)
        {
            movementType = 1;
        }
    }
}