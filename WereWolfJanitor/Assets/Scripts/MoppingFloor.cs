using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoppingFloor : MonoBehaviour
{
    [SerializeField] GameObject gm;
    private SpriteRenderer rnderer;
    [SerializeField] Sprite bloodFloor;
    [SerializeField] Sprite wetFloor;
    [SerializeField] Sprite regFloor;
    //[SerializeField] Sprite dirtyFloor; MAYBE DIRTY FLOOR INSTEAD OF ALL TILES?
    [SerializeField] GameObject bucket;
    [SerializeField] GameObject player;
    [SerializeField] GameObject soundManager;
    [SerializeField] GameObject mop;
    private Animator mopAnim;
    private GameObject prompt;
    [SerializeField] List<Sprite> uiSprites = new List<Sprite>();
    private int countP = 0;//for prompt
    private int count = 0;//for functionality
    private int bloodC = 0;

    private bool colliding = false;
    private bool pressedE = false;
    private bool pressedR = false;
    private bool pressedT = false;


    // Start is called before the first frame update

    void Start()
    {
        rnderer = GetComponent<SpriteRenderer>();
        /*if (GameObject.Find("Mop").gameObject!=null)
        {
            mop = GameObject.Find("Mop").gameObject;
        }
        else { mop = null; }*/
        
        mopAnim = mop.GetComponent<Animator>();

        GameObject[] prompts = GameObject.FindGameObjectsWithTag("Prompt");
        foreach (GameObject p in prompts)
        {
            if (p.name.Equals("NumPrompt"))
            {
                prompt = p;
                Debug.Log("prompt = "+p.name);
            }
        }
        //prompt = gm.GetComponent<GameManager>().RetrievePrompt("Num Prompt").gameObject;
        //Debug.Log("MoppingFloor: prompt = " + prompt.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (colliding && bucket.GetComponent<Bucket>().GetFilled()&&mop!=null)
        {
            prompt.GetComponent<SpriteRenderer>().sprite = uiSprites[0];
            pressedE = PressE(pressedE);
            if (pressedE)
            {
                /*prompt.GetComponent<SpriteRenderer>().sprite = uiSprites[1];
                pressedR = PressR(pressedR);
                if (pressedR)
                {
                    prompt.GetComponent<SpriteRenderer>().sprite = uiSprites[2];
                    pressedT = PressT(pressedT);
                    if (pressedT)
                    {*/
                        rnderer.sprite = wetFloor;
                        count++;
                        if (count == 1)
                        {
                            bucket.GetComponent<Bucket>().DrainBucket();
                            soundManager.GetComponent<SoundManagerScript>().PlaySound("Mopping");
                            mopAnim.SetTrigger("isMopping");
                            
                            GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
                            
                            if (bloodC!=0){ gm.GetComponent<GameManager>().IncreaseScore(6); }//increaseForBloodyFloor
                            else { gm.GetComponent<GameManager>().IncreaseScore(2); }//increaseForRegFloor
                            
                            gm.GetComponent<GameManager>().IncreaseCount(this.gameObject);
                            bloodC = 0;
                        }
                    /*}
                }*/
            }
        }
        else if (!bucket.GetComponent<Bucket>().GetFilled())
        {
            prompt.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (rnderer.sprite == bloodFloor && bloodC==0)//if it's a bloody tile, decrease score
        {
            GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
            gm.GetComponent<GameManager>().IncreaseScore(-1);
            gm.GetComponent<GameManager>().IncreaseCount(this.gameObject);
            bloodC++;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mop"))
        {
            Debug.Log("Successfully identified Mop");
            colliding = true;
            //mop = collision.gameObject;
            if (countP==0)
            {
                GameObject[] prompts = GameObject.FindGameObjectsWithTag("Prompt");
                foreach (GameObject p in prompts)
                {
                    if (p.name.Equals("NumPrompt"))
                    {
                        prompt = p;
                        Debug.Log("prompt = " + p.name);
                    }
                }
                countP++;
            }
            prompt.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            colliding = false;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mop"))
        {
            //Debug.Log("Exited tile collision");
            
            colliding = false;
            prompt.GetComponent<SpriteRenderer>().enabled = false;

        }
        
    }
    private bool PressE(bool pressedE)
    {
        if (pressedE)
        {
            //Debug.Log("moved on to Press R");
            PressR(pressedR);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("successfully pressed 1");
                pressedE = true;
                
            }
        }
        return pressedE;
    }

    private bool PressR(bool pressedR)
    {
        if (pressedR)
        {
            //Debug.Log("Pressed R now can move on to T");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("successfully pressed 2");
                pressedR = true;
                
            }
        }
        return pressedR;
    }
    private bool PressT(bool pressedT)
    {
        if (pressedT)
        {
            //Debug.Log("Finished with mopping tile");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("successfully pressed 3");
                pressedT = true;
                
            }
        }
        return pressedT;
    }
}
