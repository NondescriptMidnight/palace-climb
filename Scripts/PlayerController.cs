using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{


    private bool goingLeft = false;
    private bool goingRight = false;
    private bool jumping = false;
    private bool isJumping = false;
    private bool downing = false;
    private bool isStopped = false;
    private bool instanceOver = false;
    private bool isPlayed = false;
    private Rigidbody2D player;

    private float gravityStore;
    public static float speed = 3f;
    public float movement = 10f;
    private float climbVelocity;
    public static float climbSpeed = 70f;

    public GameObject smokePuff;
    public AudioClip crashBoom;
    public AudioClip yay;

    private int objectCount;

    // Use this for initialization
    void Start()
    {
        isPlayed = false;
        instanceOver = false;
        Debug.Log(objectCount);
        player = GetComponent<Rigidbody2D>();
        gravityStore = player.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        objectCount = GameObject.FindGameObjectsWithTag("objects").Length;
        Debug.Log(objectCount);

        if (objectCount <= 0)
        {
            if (!isPlayed)
            {
                AudioSource.PlayClipAtPoint(yay, transform.position, 1f);
                isPlayed = true;
            }
            goingLeft = false;
            goingRight = false;
            instanceOver = true;
            Invoke("Reset", 2f);
        }
        if (!instanceOver)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                goingLeft = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                goingRight = true;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumping = true;
            }

            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                goingRight = false;
                goingLeft = false;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                goingLeft = false;
                goingRight = false;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                jumping = false;
            }
        }
    }

    void FixedUpdate()
    {

        if (isJumping)
        {
            climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
            Debug.Log("Jump!");
            player.velocity = new Vector3(0, climbVelocity, 0) * Time.deltaTime;
            isJumping = true;
            player.gravityScale = 0f;
        }
        else if (!isJumping)
        {
            player.gravityScale = gravityStore;
        }

        if (goingLeft)
        {
            transform.localScale = new Vector3(-0.3f, 0.3f, 1);
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
        if (goingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }
    }
    void OnTriggerEnter2D(Collider2D colliderCheck)
    {
        if (colliderCheck.gameObject.tag == "Ladders")
        {
            isJumping = true;
        }
        ObjectBehavior collectables = colliderCheck.gameObject.GetComponent<ObjectBehavior>();
        if (collectables)
        {
            ScoreTrackering.scoreNum += 1;
        }
    }
    void OnCollisionEnter2D(Collision2D boomBang)
    {
        ObjectBehavior objects = boomBang.gameObject.GetComponent<ObjectBehavior>();
        if (boomBang.gameObject.tag == "Bombs")
        {
            AudioSource.PlayClipAtPoint(crashBoom, transform.position, 1.0f);
            GameObject smokeEffect = Instantiate(smokePuff, transform.position, Quaternion.identity) as GameObject;
            Destroy(smokeEffect, 1f);
            isStopped = true;
            Invoke("LoadLevel", 1f);
            Destroy(boomBang.gameObject);
            Destroy(gameObject.GetComponent<SpriteRenderer>());
        }
    }

    void OnTriggerExit2D(Collider2D colliderCheck)
    {
        if (colliderCheck.gameObject.tag == "Ladders")
        {
            isJumping = false;
        }
    }
    void Reset()
    {
        climbSpeed += 10;    
        speed += 0.3f;
        BombScript.bombRate -= 2.5f;
        BombScript.bombStart -= 3f;
        Application.LoadLevel(0);
    }


    void LoadLevel()
    {
        climbSpeed = 70f;
        speed = 3f;
        BombScript.bombRate = 15f;
        BombScript.bombStart = 8f;
        Application.LoadLevel(1);
    }
}


