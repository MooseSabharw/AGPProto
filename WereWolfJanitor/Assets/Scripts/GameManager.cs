using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Pathfinding;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject soundManager;
    [SerializeField] GameObject lossScreen;
    [SerializeField] List<GameObject> endOfDay = new List<GameObject>();
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject richard;
    [SerializeField] GameObject aStar;
    public int pauseSpeed = 1;
    
    public GameObject[] monsters;
    [SerializeField] float pauseTime;

    public int numberButtonsPushed = 0;
    public bool canPressWhiteButton = false;

    //new variables
    private int points = 0;
    private int trashCount = 0;
    private int moppedCount = 0;

    public GameObject[] prompts;
   

    private void Start()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster");

        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene("TitleScreen");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /*if (Input.GetKey(KeyCode.P))
        {
            if (pauseSpeed == 1)
            {
                pauseSpeed = 0;
                pausePanel.SetActive(true);
                foreach (GameObject creature in monsters)
                {
                    creature.SetActive(false);
                }
                richard.GetComponent<AIPath>().canMove = false;
                richard.GetComponent<RandomWalk>().enabled = false;
                richard.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            //Play
            else
            {
                pauseSpeed = 1;
                pausePanel.SetActive(false);
                foreach (GameObject creature in monsters)
                {
                    creature.SetActive(true);
                }
                richard.GetComponent<AIPath>().canMove = true;
                richard.GetComponent<RandomWalk>().enabled = true;
                richard.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }*/
    }
    internal void DecreaseButtonCount()//TEMP SOLUTION
    {
        throw new NotImplementedException();
    }

    public int IncreaseScore(int p)
    {
        points += p;
        Debug.Log("Points Total: " + points);
        return points;
    }

    public void IncreaseCount(GameObject obj)
    {
        if (obj.CompareTag("Trash")) {
            trashCount++;
            Debug.Log("IncreasedTrashCount: " + trashCount);
        }
        else if (obj.CompareTag("Trashbag") && obj.GetComponent<QPathFinder.MovingAI>()!=null)
        {
            trashCount += 30;
            Debug.Log("IncreasedTrashCount: " + trashCount);
        }
        else if (obj.CompareTag("Trashbag"))
        {
            trashCount += 5;
            Debug.Log("IncreasedTrashCount: " + trashCount);
        }
        else if (obj.CompareTag("Floortile"))
        {
            moppedCount++;
            Debug.Log("IncreasedFloorTileCount: " + moppedCount);
        }
        
    }

    public int GetTrashCount()
    {
        return trashCount;
    }
    public int GetMoppedCount()
    {
        return moppedCount;
    }


    public void EndOfDay()
    {
        soundManager.GetComponent<SoundManagerScript>().PlaySound("PlayerDeathSFX");
        //endOfDay.transform.position = player.transform.position;
        
        for (int i = 0; i<endOfDay.Count;i++)
        {
            endOfDay[i].SetActive(true);
        }
        player.SetActive(false);
        mainCamera.GetComponent<AudioListener>().enabled = true;
        StartCoroutine(GOCredits());
    }

    public GameObject RetrievePrompt(String promptName)
    {
        Debug.Log("Starting retrieving process");
        GameObject p = prompts[0];
        foreach(GameObject pro in prompts){
            if (pro.name.Equals(promptName))
            {
                p = pro;
                Debug.Log("Retrieving "+p.name);
            }
            Debug.Log("Trying to retrieve");
        }
        return p.gameObject;
    }
    
    public void PlayerDeath()
    {
        soundManager.GetComponent<SoundManagerScript>().PlaySound("PlayerDeathSFX");
        lossScreen.transform.position = player.transform.position;
        lossScreen.GetComponent<Image>().enabled = true;
        player.SetActive(false);
        mainCamera.GetComponent<AudioListener>().enabled = true;
        StartCoroutine(GOCredits());
    }
    
    public void RandomizeMonsters()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach(GameObject creature in monsters){
            StartCoroutine(PauseMonster(creature));
        }

    }


    IEnumerator PauseMonster(GameObject creature)
    {
        creature.SetActive(false);
        //sets to original positions
        Vector3 newTrans = new Vector3(creature.GetComponent<DickieBoiAI>().GetPositionX(), creature.GetComponent<DickieBoiAI>().GetPositionY(), creature.GetComponent<DickieBoiAI>().GetPositionZ());
        creature.transform.position = newTrans;
        yield return new WaitForSeconds(pauseTime);
        creature.SetActive(true);
    }

    IEnumerator GOCredits()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }
}
