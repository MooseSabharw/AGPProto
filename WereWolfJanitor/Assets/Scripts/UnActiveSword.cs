using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnActiveSword : MonoBehaviour
{
    private SpriteRenderer rnderer;
    private bool pressed = false;
    private bool colliding = false;
    [SerializeField] GameObject obj;

    [SerializeField] int bananapeel;
    [SerializeField] int chipbag;
    [SerializeField] int sodacan;
    [SerializeField] int paperball;
    [SerializeField] int gum;
    [SerializeField] GameObject prompt;
    private GameObject soundManager;

    // Start is called before the first frame update
    void Start()
    {
        rnderer = gameObject.GetComponent<SpriteRenderer>();
        soundManager = GameObject.FindWithTag("SoundManager");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&colliding)
        {
            //Debug.Log("pressed E");
            pressed = true;
        }
       
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("collided w/ pickUpAble Obj");
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("player obj collision");
            colliding = true;
            if (this.CompareTag("Trash")) { prompt.GetComponent<SpriteRenderer>().enabled = true; }
            
            if (pressed)
            {
                //Debug.Log("I'm before the if block");
                if (collision.gameObject.GetComponent<Inventory>().getCurrentInv() < collision.gameObject.GetComponent<Inventory>().getInventoryCap())
                {
                    soundManager.GetComponent<SoundManagerScript>().PlaySound("PickUp");
                    collision.gameObject.GetComponent<Inventory>().increaseInv();
                    obj.GetComponent<Sword>().enabled = true;
                    collision.gameObject.GetComponent<PlayerMovement>().setHoldingObj(true);
                    
                    this.GetComponent<UnActiveSword>().enabled = false;
                }
                else
                {
                    Debug.Log("Could not pick up-- Inventory full");
                }
                if (this.gameObject.CompareTag("Bucket"))
                {
                    //no adding to inventory
                    soundManager.GetComponent<SoundManagerScript>().PlaySound("PickUp");
                    Debug.Log("Holding bucket");
                    obj.GetComponent<Sword>().enabled = true;
                    collision.gameObject.GetComponent<PlayerMovement>().setHoldingObj(true);
                    
                    this.GetComponent<UnActiveSword>().enabled = false;
                }
                if (this.gameObject.CompareTag("Trash"))
                {
                    GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
                    if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name.Equals("Trash_BananaPeel"))
                    {
                        gm.GetComponent<GameManager>().IncreaseScore(bananapeel);
                    }
                    else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name.Equals("Trash_ChipBag"))
                    {
                        gm.GetComponent<GameManager>().IncreaseScore(chipbag);
                    }
                    else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name.Equals("Trash_Gum"))
                    {
                        gm.GetComponent<GameManager>().IncreaseScore(gum);
                    }
                    else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name.Equals("Trash_PaperBall"))
                    {
                        gm.GetComponent<GameManager>().IncreaseScore(paperball);
                    }
                    else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name.Equals("Trash_SodaCan"))
                    {
                        gm.GetComponent<GameManager>().IncreaseScore(sodacan);
                    }
                }
            }
        }
        
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("exiting collision");
        colliding = false;
        pressed = false;
        if (this.CompareTag("Trash")) { prompt.GetComponent<SpriteRenderer>().enabled = false; }
    }
}
