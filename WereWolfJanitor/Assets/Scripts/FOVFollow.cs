using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;

    void Update()
    {
        transform.position = player.transform.position;
    }
}
