using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] int inventoryCap;
    
    private int currentInv;
    private bool trashReady = false;
    [SerializeField] Sprite trashempty;
    [SerializeField] Sprite trashfull;
    [SerializeField] Sprite outsideTrashFull;

    // Start is called before the first frame update
    void Start()
    {
        currentInv = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getInventoryCap()
    {
        return inventoryCap;
    }

    public int getCurrentInv()
    {
        return currentInv;
    }

    public bool getTrashReady()
    {
        return trashReady;
    }


    public void increaseInv()
    {
        currentInv++;
        Debug.Log("Increased "+this.name+" inventory");
        if (this.CompareTag("Trashcan") && inventoryCap <= currentInv)
        {
            trashReady = true;
            this.GetComponent<SpriteRenderer>().sprite = trashfull;
        }
    }

    public void emptyInv()//to change to return int
    {
        currentInv = 0;
        Debug.Log("Emptied "+this.name+" inventory: " + currentInv);
        if (this.CompareTag("Trashcan") && inventoryCap >= currentInv)
        {
            trashReady = false;
            this.GetComponent<SpriteRenderer>().sprite = trashempty;
        }
    }
}
