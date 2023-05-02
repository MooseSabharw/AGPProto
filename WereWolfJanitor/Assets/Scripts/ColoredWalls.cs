using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ColoredWalls : MonoBehaviour
{

    public Sprite CollapsedWall;

    public void buttonPressed()
    {
        Debug.Log("buttonPressed() called");
        GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CollapsedWall;
        gameObject.layer = LayerMask.NameToLayer("Default");
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }
}
