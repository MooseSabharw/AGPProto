using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    private bool filled = true;
    private bool colliding = false;
    private bool pressed = false;

    [SerializeField] int numUses;
    private int numUsesStart;
    [SerializeField] Sprite emptyBucket;
    [SerializeField] Sprite regBucket;
    [SerializeField] GameObject player;
    [SerializeField] GameObject soundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        numUsesStart = numUses;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && colliding)
        {
            Debug.Log("pressed bucket E");
            pressed = true;
        }
        if (numUses==0||numUses<0)
        {
            //Debug.Log("Bucket uses 0");
            filled = false;
            this.GetComponent<SpriteRenderer>().sprite = emptyBucket;
            //send messages that can't mop unti you 
        }
        /*if (Input.GetKeyDown("r"))//release bucket
        {
            Debug.Log("pressed bucket R");
            player.gameObject.GetComponent<Inventory>().emptyInv();
            player.GetComponent<PlayerMovement>().setHoldingObj(false);
            this.GetComponent<Sword>().enabled = false;
            this.GetComponent<UnActiveSword>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Bucket: should work");
        }*/
    }

    public void FillBucket(GameObject sink)
    {
        filled = true;
        numUses = numUsesStart;
        this.GetComponent<SpriteRenderer>().sprite = regBucket;
        soundManager.GetComponent<SoundManagerScript>().PlaySound("FaucetOn");
        sink.GetComponent<Animator>().SetTrigger("FaucetOn"); //trigger anim for running sink tap
        Debug.Log("Filled bucket");
    }

    public bool GetFilled()
    {
        return filled;
    }

    public void DrainBucket()
    {
        numUses--;
    }
}
