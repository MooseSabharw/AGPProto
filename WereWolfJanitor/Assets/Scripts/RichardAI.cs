using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RichardAI : MonoBehaviour
{
    [SerializeField] GameObject richard;
    [SerializeField] GameObject aStar;
    private int pauseSpeed;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            Debug.Log("Player entered the collision zone");
            aStar.SetActive(true);
            //richard.GetComponent<AIPath>().canMove = false;
            aStar.GetComponent<AstarPath>().enabled = true;
            richard.GetComponent<AIPath>().enabled = true;
            richard.GetComponent<AIDestinationSetter>().enabled = true;
            richard.GetComponent<Seeker>().enabled = true;
            richard.GetComponent<RandomWalk>().enabled = false;
            richard.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            Debug.Log("Player exited collision");
            //richard.GetComponent<AIPath>().canMove = true;
            aStar.SetActive(false);
            aStar.GetComponent<AstarPath>().enabled = false;
            richard.GetComponent<AIPath>().enabled = false;
            richard.GetComponent<AIDestinationSetter>().enabled = false;
            richard.GetComponent<Seeker>().enabled = false;
            richard.GetComponent<RandomWalk>().enabled = true;
            richard.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

   
}
