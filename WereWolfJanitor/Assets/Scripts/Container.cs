using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] GameObject player;
    private GameObject gm;
    private GameObject soundManager;
    private GameObject trash;
    private bool pressed = false;
    private bool colliding = false;
    private int count = 0;
    [SerializeField] GameObject prompt;

    public List<GameObject> trashList = new List<GameObject>();
    private GameObject trashBag;
    [SerializeField] GameObject student;
    [SerializeField] GameObject teacher;
    [SerializeField] int bananapeel;
    [SerializeField] int chipbag;
    [SerializeField] int sodacan;
    [SerializeField] int paperball;
    [SerializeField] int gum;

    [SerializeField] Sprite outsidet1;
    [SerializeField] Sprite outsidet2;
    [SerializeField] Sprite outsidet3;

    [SerializeField] GameObject trashbag;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject t in GameObject.FindGameObjectsWithTag("Trash")){
            trashList.Add(t);
        }
        trashBag = GameObject.FindGameObjectWithTag("Trashbag");
        gm = GameObject.FindGameObjectWithTag("GameManager");
        soundManager = GameObject.FindWithTag("SoundManager");
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colliding)
        {
            Debug.Log("pressed E");
            pressed = true;
            
        }
        
    }

    public void OnCollisionStay2D(Collision2D other)//placing obj in other place
    {
        colliding = true;
        if (prompt!=null) { prompt.GetComponent<SpriteRenderer>().enabled = true; }

        if ((player.GetComponent<Inventory>().getCurrentInv() == 0) && pressed && this.gameObject.GetComponent<Inventory>().getTrashReady() && count==0)
        {
            count++;
            
            
        }
        
        if ((player.GetComponent<Inventory>().getCurrentInv() != 0) && pressed && count == 0)
        {
            Debug.Log("Inventory over 0");
            foreach (GameObject t in trashList)
            {
                Debug.Log(t.name);
                if (t.GetComponent<Sword>().enabled)
                {

                    Debug.Log("Trash = t");
                    trash = t.gameObject;
                    break;
                }
                
            }
            if (trashBag.GetComponent<Sword>().enabled)
            {
                Debug.Log("Trash = trashbag");
                trash = trashbag;
            }
            if (student.GetComponent<Sword>().enabled)
            {
                Debug.Log("Trash = student");
                trash = student;
            }
            if (teacher.GetComponent<Sword>().enabled)
            {
                Debug.Log("Trash = teacher");
                trash = teacher;
            }
            Debug.Log(trash.name+ " is what I'm trying to place");//properly finds trashbag in list MOST OF THE TIME

            if ((this.gameObject.CompareTag("Cart") || this.gameObject.CompareTag("Trashcan")) && trash.gameObject.CompareTag("Trash") && trash.GetComponent<QPathFinder.MovingAI>() == null)
            {
                count++; //prevents immediately taking the trash out of the can
                Debug.Log("Container first if");

                if (this.gameObject.CompareTag("Cart") && trash.GetComponent<QPathFinder.MovingAI>()==null)
                {
                    if (this.gameObject.GetComponent<Inventory>().getCurrentInv() < this.gameObject.GetComponent<Inventory>().getInventoryCap())
                    {
                        soundManager.GetComponent<SoundManagerScript>().PlaySound("PickUp");
                        this.gameObject.GetComponent<Inventory>().increaseInv();
                        Debug.Log("Added to Cart");
                        //NEED TO DELETE THE OBJ

                        player.gameObject.GetComponent<Inventory>().emptyInv();
                        player.GetComponent<PlayerMovement>().setHoldingObj(false);
                        trash.GetComponent<Sword>().enabled = false;
                        trash.GetComponent<SpriteRenderer>().enabled = false;
                        gm.GetComponent<GameManager>().IncreaseCount(trash);
                    }
                    else
                    {
                        Debug.Log("Could not add to cart");
                    }

                }
                if (this.gameObject.CompareTag("Trashcan") && trash.GetComponent<QPathFinder.MovingAI>() == null)
                {
                    soundManager.GetComponent<SoundManagerScript>().PlaySound("PickUp");
                    this.gameObject.GetComponent<Inventory>().increaseInv();
                    player.gameObject.GetComponent<Inventory>().emptyInv();
                    Debug.Log("Emptied player inventory");
                    //NEED TO DELETE THE OBJ
                    
                    player.GetComponent<PlayerMovement>().setHoldingObj(false);
                    trash.GetComponent<Sword>().enabled = false;
                    trash.GetComponent<SpriteRenderer>().enabled = false;
                    gm.GetComponent<GameManager>().IncreaseCount(trash);
                }
            }
            else if (this.gameObject.name.Equals("OutsideTrashbin") && (trash.CompareTag("Trashbag") || trash.GetComponent<QPathFinder.MovingAI>()!=null))
            {
                soundManager.GetComponent<SoundManagerScript>().PlaySound("PickUp");
                this.gameObject.GetComponent<Inventory>().increaseInv();
                player.gameObject.GetComponent<Inventory>().emptyInv();
                //Debug.Log("Emptied player inventory");
                //change sprite of outsidebin
                if (this.gameObject.GetComponent<Inventory>().getCurrentInv() == 1) { this.GetComponent<SpriteRenderer>().sprite = outsidet1; }
                else if (this.gameObject.GetComponent<Inventory>().getCurrentInv() == 2) { this.GetComponent<SpriteRenderer>().sprite = outsidet2; }
                else if (this.gameObject.GetComponent<Inventory>().getCurrentInv() == 3) { this.GetComponent<SpriteRenderer>().sprite = outsidet3; }

                if (trash.CompareTag("Trashbag"))
                {
                    trashBag.GetComponent<Sword>().enabled = false;
                    trashBag.GetComponent<SpriteRenderer>().enabled = false;
                }
                else if (trash.GetComponent<QPathFinder.MovingAI>() != null)
                {
                    trash.GetComponent<Sword>().enabled = false;
                    trash.GetComponent<SpriteRenderer>().enabled = false;
                }
                player.GetComponent<PlayerMovement>().setHoldingObj(false);
                gm.GetComponent<GameManager>().IncreaseCount(trash);
                Debug.Log("placed trashbag in outside trashbin");
            }
            else
            {
                Debug.Log("Not something somewhere I can place");
            }
        }
        else
        {
            Debug.Log("Did not place");
            Debug.Log("Current Inventory: " + player.GetComponent<Inventory>().getCurrentInv());
            
        }

    }



    public void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Container: exiting collision");
        colliding = false;
        pressed = false;
        count = 0;
        if (prompt != null) { prompt.GetComponent<SpriteRenderer>().enabled = false; }
    }

    public int GetCount()
    {
        return count;
    }
}
