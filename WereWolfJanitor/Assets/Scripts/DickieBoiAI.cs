using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DickieBoiAI : MonoBehaviour
{
    [SerializeField] float speed;
    private int pauseSpeed;
    private float x;//x value translate
    private float y;//y value translate
    private bool stopped = false;//currently colliding with something
    private int rand;//1-4 to pick direction
    private int newRand;//whileloop checker
    private SpriteRenderer renderer;
    [SerializeField] Sprite horizontal;
    [SerializeField] Sprite vertical;
    private Animator anim;
    public float origPositionX;
    public float origPositionY;
    public float origPositionZ;

    // Start is called before the first frame update
    void Start()
    {
        origPositionX = gameObject.transform.position.x;
        origPositionY = gameObject.transform.position.y;
        origPositionZ = gameObject.transform.position.z;

        renderer = gameObject.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rand = Random.Range(0, 4);
        if (rand == 1)//move left
        {
            renderer.sprite = horizontal;
            anim.SetBool("isHorizontal", true);
            renderer.flipX = true;
            x = -speed;
            y = 0f;
        }
        else if (rand == 2)//move up
        {
            renderer.sprite = vertical;
            anim.SetBool("isHorizontal", false);
            renderer.flipY = false;
            x = 0f;
            y = speed;
        }
        else if (rand == 3)//move right
        {
            renderer.sprite = horizontal;
            anim.SetBool("isHorizontal", true);
            renderer.flipX = false;
            x = speed;
            y = 0f;
        }
        else//move down
        {

            renderer.sprite = vertical;
            anim.SetBool("isHorizontal", false);
            renderer.flipY = true;
            x = 0f;
            y = -speed;
        }
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (!stopped)
        {
            transform.Translate(x, y, 0f);
        }
        else
        {
            newRand = Random.Range(0, 4);
            while (rand == newRand)
            {
                newRand = Random.Range(0, 4);
            }
            rand = newRand;
            if (rand == 1)//move left
            {
                renderer.sprite = horizontal;
                anim.SetBool("isHorizontal", true);
                renderer.flipX = true;
                renderer.flipY = false;
                x = -speed;
                y = 0f;
            }
            else if (rand == 2)//move up
            {
                renderer.sprite = vertical;
                anim.SetBool("isHorizontal", false);
                renderer.flipY = false;
                x = 0f;
                y = speed;
            }
            else if (rand == 3)//move right
            {
                renderer.sprite = horizontal;
                anim.SetBool("isHorizontal", true);
                renderer.flipX = false;
                renderer.flipY = false;
                x = speed;
                y = 0f;
            }
            else//move down
            {
                renderer.sprite = vertical;
                anim.SetBool("isHorizontal", false);
                renderer.flipY = true;
                x = 0f;
                y = -speed;
            }
            stopped = false;
        }
    }

    public void SetStop(bool value)
    {
        stopped = value;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        stopped = true;
        x *= 10;
        y *= 10;
        transform.Translate(-x, -y, 0f);
    }

    public void OnCollisionStay(Collision collision)
    {
        
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        stopped = false;
    }

    public float GetPositionX()
    {
        return origPositionX;
    }
    public float GetPositionY()
    {
        return origPositionY;
    }
    public float GetPositionZ()
    {
        return origPositionZ;
    }
}
