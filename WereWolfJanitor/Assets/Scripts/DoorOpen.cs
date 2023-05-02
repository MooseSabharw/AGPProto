using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen: MonoBehaviour
{
    private bool colliding = false;
    private bool pressed = false;
    private bool opened = false;

    [SerializeField] Sprite openedDoor;
    [SerializeField] Sprite closedDoor;

    [SerializeField] GameObject closeTrigger;
    [SerializeField] GameObject prompt;
    private GameObject soundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindWithTag("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colliding)
        {
            Debug.Log("pressed E");
            pressed = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colliding = true;
            prompt.GetComponent<SpriteRenderer>().enabled = true;
            if (pressed && !opened) //open door
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
                this.GetComponent<SpriteRenderer>().sprite = openedDoor;
                soundManager.GetComponent<SoundManagerScript>().PlaySound("OpenDoor");
                opened = true;
                closeTrigger.SetActive(true);
            }
            
        }
    }

    

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("exiting collision");
        colliding = false;
        pressed = false;
        prompt.GetComponent<SpriteRenderer>().enabled = false;
    }

    

    public bool getOpened()
    {
        return opened;
    }

    public void closeDoor()
    {
        if (opened) {
            this.GetComponent<SpriteRenderer>().sprite = closedDoor;
            this.GetComponent<BoxCollider2D>().enabled = true;
            opened = false;
            closeTrigger.SetActive(false);
        }
       
    }

    
}
