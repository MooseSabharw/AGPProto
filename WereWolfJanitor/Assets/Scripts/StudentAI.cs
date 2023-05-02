using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QPathFinder
{
public class StudentAI : MonoBehaviour
{
    [SerializeField] GameObject studentMover;
        private bool colliding1 = false;
        private bool colliding2 = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            if (colliding1)
            {
                //studentMover.GetComponent<MoveToNode>().TriggerMoveTo("colliding1");
            }
            if (colliding2)
            {
                //studentMover.GetComponent<MoveToNode>().TriggerMoveTo("colliding2");
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("Student colliding with something");
            if (collision.gameObject.name.Equals("StuPathTrigger1"))
            {
                Debug.Log("StuPathTrigger1 triggered");
                colliding1 = true;
                //studentMover.GetComponent<MoveToNode>().TriggerMoveTo(PathFinder.instance.graphData.nodes[6]);
             }
            if (collision.gameObject.name.Equals("StuPathTrigger2"))
            {
                Debug.Log("StuPathTrigger2 triggered");
                colliding2 = true;
                //studentMover.GetComponent<MoveToNode>().TriggerMoveTo(PathFinder.instance.graphData.nodes[0]);
            }
    }

        private void OnTriggerExit2D(Collider2D collision)
        {
            colliding1 = false;
            colliding2 = false;
        }
    }
}
