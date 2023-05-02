using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RichardGFX : MonoBehaviour
{
    public AIPath aiPath;
    private SpriteRenderer rnderer;

    private void Start()
    {
        rnderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f) //right
        {
            rnderer.flipX = false;
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)//left
        {
            rnderer.flipX = true;
        }
        
    }
}
