using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int pauseSpeed;
    [SerializeField] float speed;
    private Vector2 speedX;
    private Vector2 speedY;
    private Rigidbody2D rb2D;
    private BoxCollider2D bcollider;
    private bool left;
    private bool right;
    private bool up;
    private bool down;
    private bool isMoving;
    [SerializeField] GameObject sword;

    public int fuseCount;
    public GameObject FOVObject;
    public GameObject winScreen;
    [SerializeField] Sprite sideSprite;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;
    private Animator anim;
    private int count = 0;
    private SpriteRenderer rnderer;
    [SerializeField] SoundManagerScript soundManager;
    [SerializeField] GameManager gm;


    [SerializeField] GameObject mop;
    [SerializeField] GameObject cartAttached;
    private SpriteRenderer mopRenderer;
    private SpriteRenderer cartRenderer;
    [SerializeField] Sprite mopSide;
    [SerializeField] Sprite mopUp;
    [SerializeField] Sprite mopDown;
    private Animator mopAnim;
    private BoxCollider2D mopCollider;
    [SerializeField] Sprite cartSide;
    [SerializeField] Sprite cartUp;
    [SerializeField] Sprite cartDown;

    [SerializeField] bool holdingObj;
    private bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rnderer = gameObject.GetComponent<SpriteRenderer>();
        speedX = new Vector2(speed*pauseSpeed, 0);
        speedY = new Vector2(0, speed*pauseSpeed);
        bcollider = GetComponent<BoxCollider2D>();

        mopRenderer = mop.GetComponent<SpriteRenderer>();
        mopAnim = mop.GetComponent<Animator>();
        mopCollider = mop.GetComponent<BoxCollider2D>();
        cartRenderer = cartAttached.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseSpeed == 1)
            {
                pauseSpeed = 0;
            }
            //Play
            else
            {
                pauseSpeed = 1;
            }
        }*/
        speedX = new Vector2(speed*Time.deltaTime, 0);
        speedY = new Vector2(0, speed*Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (isPaused)
        {
            speedX = new Vector2(0, 0);
            speedY = new Vector2(0, 0);
            rb2D.MovePosition(rb2D.position);//prevents floating
            anim.SetBool("isMoving", false);
            count = 0;
            isMoving = false;
            Debug.Log("NPC not moving");
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            count++;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                
                anim.SetBool("isMoving", true);
                anim.SetBool("Side", false);
                anim.SetBool("Up", true);
                rnderer.flipX = false;
                bcollider.size = new Vector2(1.47f, 4.989557f);
                mopRenderer.flipX = false;
                mopRenderer.sortingOrder = 6;
                mopCollider.offset = new Vector2(0f, -2.236133f);
                //mopRenderer.sprite = mopUp;
                
                mopAnim.SetBool("Side", false);
                mopAnim.SetBool("Up", true);
                cartRenderer.flipX = false;
                cartRenderer.sortingOrder = 6;
                cartRenderer.sprite = cartUp;
                up = true;
                down = false;
                right = false;
                left = false;
                isMoving = true;
            }
            rb2D.MovePosition(rb2D.position + speedY);
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))//flip x
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
               
                anim.SetBool("isMoving", true);
                anim.SetBool("Side", true);
                anim.SetBool("Up", false);
                rnderer.flipX = true;
                mopRenderer.flipX = true;
                bcollider.size = new Vector2(1.47f, 4.989557f);
                mopRenderer.sortingOrder = 8;
                mopCollider.offset = new Vector2(-0.79982f, -2.236133f);
                //mopRenderer.sprite = mopSide;

                mopAnim.SetBool("Side", true);
                mopAnim.SetBool("Up", false);
                cartRenderer.flipX = true;
                cartRenderer.sortingOrder = 8;
                cartRenderer.sprite = cartSide;
                up = false;
                down = false;
                right = false;
                left = true;
                isMoving = true;
            }
            rb2D.MovePosition(rb2D.position - speedX);

        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                
                anim.SetBool("isMoving", true);
                anim.SetBool("Side", false);
                anim.SetBool("Up", false);
                rnderer.flipX = false;
                mopRenderer.flipX = false;
                bcollider.size = new Vector2(2.313023f, 4.989557f);
                mopRenderer.sortingOrder = 8;
                mopCollider.offset = new Vector2(0f, -2.236133f);
                //mopRenderer.sprite = mopDown;

                mopAnim.SetBool("Side", false);
                mopAnim.SetBool("Up", false);
                cartRenderer.flipX = false;
                cartRenderer.sortingOrder = 8;
                cartRenderer.sprite = cartDown;
                up = false;
                down = true;
                right = false;
                left = false;
                isMoving = true;
            }
            rb2D.MovePosition(rb2D.position - speedY);
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                
                anim.SetBool("isMoving", true);
                anim.SetBool("Side", true);
                anim.SetBool("Up", false);
                rnderer.flipX = false;
                bcollider.size = new Vector2(2.313023f, 4.989557f);
                mopRenderer.flipX = false;
                mopRenderer.sortingOrder = 8;
                mopCollider.offset = new Vector2(0.79982f, -2.236133f);
                //mopRenderer.sprite = mopSide;

                mopAnim.SetBool("Side", true);
                mopAnim.SetBool("Up", false);
                cartRenderer.flipX = false;
                cartRenderer.sortingOrder = 8;
                cartRenderer.sprite = cartSide;
                up = false;
                down = false;
                right = true;
                left = false;
                isMoving = true;
            }
            rb2D.MovePosition(rb2D.position + speedX);
        }
        else
        {
            rb2D.MovePosition(rb2D.position);//prevents floating
            anim.SetBool("isMoving", false);
            count = 0;
            isMoving = false;
            Debug.Log("NPC not moving");
        }

        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Monster" || other.gameObject.tag == "Richard")
        {
            sword.SetActive(false);
            gm.GetComponent<GameManager>().PlayerDeath();
        }
    }

    public void SetPaused(bool newp)
    {
        isPaused = newp;
    }
    
    public bool getHoldingObj()
    {
        return holdingObj;
    }

    public void setHoldingObj(bool holding)
    {
        holdingObj = holding;
    }

    public void UsedFuse()
    {
        fuseCount = fuseCount + 1;
        if (fuseCount >= 4)
        {
            winScreen.SetActive(true);
        }
        FOVObject.SetActive(false);
        Invoke("ReEnableFOVObject", 7f);
    }

    private void ReEnableFOVObject()
    {
        FOVObject.SetActive(true);
    }

    public bool IsLeft()
    {
        return left;
    }
    public bool IsRight()
    {
        return right;
    }
    public bool IsUp()
    {
        return up;
    }
    public bool IsDown()
    {
        return down;
    }
    public bool isPMoving()
    {
        return isMoving;
    }
}
