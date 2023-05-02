using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    private float vectorX;
    private float vectorY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.x < 14f && target.position.x > -14f) {
            vectorX = target.transform.position.x;
        }
        if (target.position.y > -14f && target.position.y < 14f) {
            vectorY = target.transform.position.y;
        }
        if (target.position.x > 14f)
        {
            vectorX = 14f;
        }
        if (target.position.x < -14f)
        {
            vectorX = -14f;
        }
        if (target.position.y > 10f)
        {
            vectorY = 10f;
        }
        if (target.position.y < -14f)
        {
            vectorY = -14f;
        }
        transform.position = new Vector3(vectorX, vectorY, transform.position.z);
    }
}
