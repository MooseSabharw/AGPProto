using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    [SerializeField] float speed;
    private float x;//x value translate
    private float y;//y value translate
    private bool stopped = true;//currently colliding with something
    private int rand;//1-4 to pick direction
    private int newRand;//whileloop checker
    private SpriteRenderer rnderer;
    private Rigidbody2D rb2D;
    private Vector2 speedX;
    private Vector2 speedY;

    [SerializeField] GameObject richardGFX;

    // Start is called before the first frame update
    void Start()
    {
        rnderer = richardGFX.GetComponent<SpriteRenderer>();
        rand = Random.Range(0, 4);
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        speedX = new Vector2(speed, 0);
        speedY = new Vector2(0, speed);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ADD SOMETHING TO CHECK FOR OBSTACLE LAYER MASK AND SAY NOT TO WALK ON THEM
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (stopped)
        {
            newRand = Random.Range(0, 4);
            while (rand == newRand)
            {
                newRand = Random.Range(0, 4);
            }
            rand = newRand;
            stopped = false;
            Debug.Log("I picked a random number");
        }
        if (rand == 2)//move up
        {
            Debug.Log("moving up");
            //transform.position = (new Vector3(0,speed,0)*Time.deltaTime);
            rb2D.MovePosition(rb2D.position + speedY);
        }
        else if ((rand == 1))//move left
        {
            Debug.Log("moving left");
            rnderer.flipX = true;
            //transform.position = (new Vector3(speed, 0, 0) * Time.deltaTime);
            rb2D.MovePosition(rb2D.position - speedX);

        }
        else if (rand == 3)//move right
        {
            Debug.Log("moving right");
            rnderer.flipX = false;
            //transform.position = (new Vector3(-speed, 0, 0) * Time.deltaTime);
            rb2D.MovePosition(rb2D.position + speedX);
        }
        else //move down
        {
            Debug.Log("moving down");
            //transform.position = (new Vector3(0, -speed, 0) * Time.deltaTime);
            rb2D.MovePosition(rb2D.position - speedY);
        }
    }

    public void SetStop(bool value)
    {
        stopped = value;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("i hit something");
        stopped = true;
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        stopped = false;
    }
}
