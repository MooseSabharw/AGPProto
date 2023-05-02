using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChildren : MonoBehaviour
{
    private List<Transform> children = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform child in transform)
        {
            Debug.Log("Child "+child.name);
            children.Add(child);
        }

        Debug.Log("Count: " + children.Count);
        //RetrieveChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject RetrieveChild(int i)
    {
        Debug.Log("Child retrieved: " + children[i].gameObject.name);
        return children[i].gameObject;
    }
}
