using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachment : MonoBehaviour
{
    //Script changed to be specifically for the cart not both cart and mop as separate objs
    private bool hasMop = true;
    private bool equipped = false;

    [SerializeField] GameObject player;
    private SpriteRenderer rnderer;
    [SerializeField] Sprite withoutMop;
    [SerializeField] Sprite withMop;
    [SerializeField] GameObject attachedCartObj;


    private bool up;
    private bool down;
    private bool left;
    private bool right;

    private bool oldUp;
    private bool oldDown;
    private bool oldLeft;
    private bool oldRight;

    private bool upOrLeft;
    [SerializeField] float adjX; //for adjusting placement of obj
    [SerializeField] float adjY;

    // Start is called before the first frame update
    void Start()
    {
        rnderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (equipped) //unattachedcart follows player when inactive
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
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (down || right)
                {
                    upOrLeft = false;
                    rnderer.flipX = false;
                    rnderer.sortingOrder = 8;
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            if (player.GetComponent<PlayerMovement>().isPMoving())
            {
                Moving();
            }
        }

        
    }

   
    public void ReleaseCart() //letting go of cart
    {
        Debug.Log("release cart called");
        if (equipped && Input.GetKeyDown("r") && player.GetComponent<PlayerMode>().GetCount()==0) 
        {
            equipped = false;
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
            attachedCartObj.SetActive(false);
            if (this.GetComponent<SpriteRenderer>().flipX)
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(-.4328087f, -0.6405743f);
            }
            else
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(.4328087f, -0.6405743f);
            }
            Debug.Log("released the Cart");
        }
    }

    public bool getMop()
    {
        return hasMop;
    }
    public void setEquipped(bool x)
    {
        equipped = x;
    }

    public void takeMop()
    {
        hasMop = false;
        rnderer.sprite = withoutMop;
    }
    public void returnMop()
    {
        hasMop = true;
        rnderer.sprite = withMop;
    }

    private void CheckMovement()
    {
        up = player.GetComponent<PlayerMovement>().IsUp();
        down = player.GetComponent<PlayerMovement>().IsDown();
        left = player.GetComponent<PlayerMovement>().IsLeft();
        right = player.GetComponent<PlayerMovement>().IsRight();
    }
    private void Moving()
    {
        Debug.Log("obj is moving");
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        if (upOrLeft)
        {
            gameObject.transform.position = new Vector3(x - adjX, y - adjY, 0f);
        }
        if (down || right)
        {
            gameObject.transform.position = new Vector3(x + adjX, y - adjY, 0f);
        }
    }
}
