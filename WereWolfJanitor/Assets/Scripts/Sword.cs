using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private bool up;
    private bool down;
    private bool left;
    private bool right;

    private bool oldUp;
    private bool oldDown;
    private bool oldLeft;
    private bool oldRight;

    private bool upOrLeft;

    public GameObject player;
    [SerializeField] float adjX; //for adjusting placement of obj
    [SerializeField] float adjY;


    private SpriteRenderer rnderer;
    private Animator anim;
    private BoxCollider2D bCollider;


    private bool pressed = false;
    private bool colliding = false;
    // Start is called before the first frame update
    void Start()
    {
        rnderer = this.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        bCollider = this.GetComponent<BoxCollider2D>();
        anim.enabled = false;
        bCollider.enabled = false;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        oldUp = up;
        oldDown = down;
        oldLeft = left;
        oldRight = right;
        CheckMovement();
        if (oldUp != up || oldDown != down || oldLeft != left || oldRight != right)
        {

            if (up || left)
            {
                upOrLeft = true;
                rnderer.flipX = true;
                if (left)
                {
                    rnderer.sortingOrder = 6;
                }
                else
                {
                    rnderer.sortingOrder = 8;
                }
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 40);
            }
            if (down || right)
            {
                upOrLeft = false;
                rnderer.flipX = false;
                rnderer.sortingOrder = 8;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 320);
            }
        }
        if (player.GetComponent<PlayerMovement>().isPMoving())
        {
            Moving();
        }
    }

    private void Moving()
    {
        Debug.Log("obj is moving");
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        if (upOrLeft && !(down||right))
        {
            gameObject.transform.position = new Vector3(x - adjX, y - adjY, 0f);
        }
        if ((down || right)&& !upOrLeft)
        {
            gameObject.transform.position = new Vector3(x + adjX, y - adjY, 0f);
        }
    }
    
    private void CheckMovement()
    {
        up = player.GetComponent<PlayerMovement>().IsUp();
        down = player.GetComponent<PlayerMovement>().IsDown();
        left = player.GetComponent<PlayerMovement>().IsLeft();
        right = player.GetComponent<PlayerMovement>().IsRight();
    }


}
