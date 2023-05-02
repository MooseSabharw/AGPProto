using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNPC : MonoBehaviour
{
    private bool up;
    private bool down;
    private bool left;
    private bool right;
    [SerializeField] GameObject player;

    private SpriteRenderer rnderer;
    private Animator anim;
    private BoxCollider2D bCollider;
    // Start is called before the first frame update
    void Start()
    {
        rnderer = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        bCollider = GetComponent<BoxCollider2D>();
        anim.enabled = false;
        bCollider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckMovement()
    {
        up = player.GetComponent<PlayerMovement>().IsUp();
        down = player.GetComponent<PlayerMovement>().IsDown();
        left = player.GetComponent<PlayerMovement>().IsLeft();
        right = player.GetComponent<PlayerMovement>().IsRight();
    }



    /*public void Attack()
    {
        //StartCoroutine(AttackAnim());

    }

    IEnumerator AttackAnim() //check if need moving from Sword script
    {
        float newY = 0f;
        float newX = 0f;
        float sameX;
        float sameY;
        bCollider.enabled = true;
        if (up)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            sameX = gameObject.transform.position.x;
            sameY = gameObject.transform.position.y;
            newY = gameObject.transform.position.y + 0.9f;
            yield return new WaitForSeconds(0.05f);
            gameObject.transform.position = new Vector3(sameX, newY, 0f);
            yield return new WaitForSeconds(0.05f);
            Moving();
        }
        if (left)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            sameX = gameObject.transform.position.x;
            sameY = gameObject.transform.position.y;
            newX = gameObject.transform.position.x - 0.9f;
            yield return new WaitForSeconds(0.05f);
            gameObject.transform.position = new Vector3(newX, sameY, 0f);
            yield return new WaitForSeconds(0.05f);
            Moving();
        }
        if (down)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            sameX = gameObject.transform.position.x;
            sameY = gameObject.transform.position.y;
            newY = gameObject.transform.position.y - 0.9f;
            yield return new WaitForSeconds(0.05f);
            gameObject.transform.position = new Vector3(sameX, newY, 0f);
            yield return new WaitForSeconds(0.05f);
            Moving();
        }
        if (right)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
            sameX = gameObject.transform.position.x;
            sameY = gameObject.transform.position.y;
            newX = gameObject.transform.position.x + 0.9f;
            yield return new WaitForSeconds(0.05f);
            gameObject.transform.position = new Vector3(newX, sameY, 0f);
            yield return new WaitForSeconds(0.05f);
            Moving();
        }
        bCollider.enabled = false;
    }*/

}
