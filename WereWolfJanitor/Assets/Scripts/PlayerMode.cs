using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviour
{
    private bool colliding = false;
    private int count = 0;
    private bool collidingBucket = false;
    private bool pressed = false;
    private GameObject obj;
    private GameObject player;
    [SerializeField] GameObject mopObj;
    [SerializeField] GameObject bucketObj;
    [SerializeField] GameObject attachedCartObj;
    [SerializeField] GameObject trashBag;
    [SerializeField] List<GameObject> prompts = new List<GameObject>();
    private GameObject prompt;
    private GameObject prompt2;
    private GameObject soundManager;

    private bool equipped = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        soundManager = GameObject.FindWithTag("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (colliding && Input.GetKeyDown("e") && !player.GetComponent<PlayerMovement>().getHoldingObj())
        {
            //Debug.Log("Past initial E input");
            if (obj.CompareTag("Cart"))
            {
                if (!obj.GetComponent<PlayerAttachment>().getMop()) //returning mop
                {
                    count++;
                    obj.GetComponent<PlayerAttachment>().returnMop();
                    mopObj.SetActive(false);
                    bucketObj.GetComponent<SpriteRenderer>().enabled = false;
                    bucketObj.GetComponent<BoxCollider2D>().enabled = false;
                    bucketObj.GetComponent<UnActiveSword>().enabled = false;
                    //bucketObj.transform.parent = this.transform;
                    Debug.Log("Returned mop");
                }
                else //retrieving cart
                {
                    count++;
                    equipped = true;
                    obj.GetComponent<PlayerAttachment>().setEquipped(equipped);
                    obj.GetComponent<SpriteRenderer>().enabled = false;
                    obj.GetComponent<BoxCollider2D>().enabled = false;
                    attachedCartObj.SetActive(true);
                    Debug.Log("attached the Cart");
                }
            }
            
            //getting trash from trashcans
            if (colliding && Input.GetKeyDown("e") && obj.CompareTag("Trashcan") && obj.GetComponent<Inventory>().getTrashReady() && obj.GetComponent<Container>().GetCount()==0)
            {
                count++;
                soundManager.GetComponent<SoundManagerScript>().PlaySound("PickUp");
                obj.GetComponent<Inventory>().emptyInv();
                player.GetComponent<Inventory>().increaseInv();
                player.GetComponent<PlayerMovement>().setHoldingObj(true);
                trashBag.GetComponent<SpriteRenderer>().enabled = true;
                trashBag.GetComponent<BoxCollider2D>().enabled = true;
                trashBag.GetComponent<Sword>().enabled = true;
            }
        }
        if (colliding && GetInput("M") && obj.GetComponent<PlayerAttachment>().getMop() && !player.GetComponent<PlayerMovement>().getHoldingObj())
        {
            count++;
            mopObj.SetActive(true);
            bucketObj.GetComponent<SpriteRenderer>().enabled = true;
            bucketObj.GetComponent<BoxCollider2D>().enabled = true;
            bucketObj.GetComponent<UnActiveSword>().enabled = true;
            //bucketObj.transform.parent = null;
            obj.GetComponent<PlayerAttachment>().setEquipped(equipped);
            obj.GetComponent<PlayerAttachment>().takeMop();
            //Debug.Log("Took Mop");
        }

        if (Input.GetKeyDown("e") && collidingBucket)
        {
            count++;
            //Debug.Log("Pressed E for Bucket");
            pressed = true;
        }
        if (count==0)//release cart is in PlayerAttachment in order to use release
        {
            GameObject.FindGameObjectWithTag("Cart").GetComponent<PlayerAttachment>().ReleaseCart();
        }

        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cart"))
        {
            //enable UI prompts
            
            colliding = true;
            obj = collision.gameObject;
            foreach (GameObject p in prompts)
            {
                if (p.name.Equals("E Prompt_Cart"))
                {
                    prompt = p;
                    Debug.Log("prompt = " + p.name);
                    p.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (p.name.Equals("M Prompt"))
                {
                    prompt2 = p;
                    Debug.Log("prompt2 = " + p.name);
                    if (obj.GetComponent<PlayerAttachment>().getMop())
                    {
                        p.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        }

        if (collision.gameObject.CompareTag("Sink") && bucketObj.GetComponent<Sword>().enabled)
        {
            foreach (GameObject p in prompts)
            {
                if (p.name.Equals("EPrompt_Sink")&& collision.gameObject.name.Equals("Sink"))
                {
                    prompt = p;
                    Debug.Log("prompt = " + p.name);
                    p.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (p.name.Equals("EPrompt_Sink (1)") && collision.gameObject.name.Equals("Sink (1)"))
                {
                    prompt = p;
                    Debug.Log("prompt = " + p.name);
                    p.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            collidingBucket = true;
            if (pressed)
            {
                bucketObj.GetComponent<Bucket>().FillBucket(collision.gameObject);
            }
            
        }
        if (collision.gameObject.CompareTag("Trashcan"))
        {
            colliding = true;
            obj = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cart"))
        {
            colliding = false;
            count = 0;
            prompt.GetComponent<SpriteRenderer>().enabled = false;
            prompt2.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (collision.gameObject.CompareTag("Sink") && bucketObj.GetComponent<Sword>().enabled)
        {
            prompt.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private bool GetInput(string letter)
    {
        if (letter.Equals("E")) {
            if (Input.GetKeyDown(KeyCode.E)) { return true; }
            else { return false; }
        }
        else if (letter.Equals("M"))
        {
            if (Input.GetKeyDown(KeyCode.M)) { return true; }
            else { return false; }
        }
        else
        {
            return false;
        }
    }

    public int GetCount()
    {
        return count;
    }


}
