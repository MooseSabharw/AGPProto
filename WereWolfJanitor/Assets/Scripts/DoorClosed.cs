using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosed : MonoBehaviour
{
    [SerializeField] GameObject door;
    private bool colliding = false;
    private bool pressed = false;
    private GameObject soundManager;

    private void Start()
    {
        soundManager = GameObject.FindWithTag("SoundManager");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colliding)
        {
            Debug.Log("pressed E");
            pressed = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)//closes door manually
    {
        colliding = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pressed && door.GetComponent<DoorOpen>().getOpened()) 
            { 
                door.GetComponent<DoorOpen>().closeDoor();
                soundManager.GetComponent<SoundManagerScript>().PlaySound("CloseDoor");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exiting collision");
        colliding = false;
        pressed = false;
    }
}
