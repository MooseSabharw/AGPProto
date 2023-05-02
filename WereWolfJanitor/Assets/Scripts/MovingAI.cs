using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QPathFinder
{
    public class MovingAI : MonoBehaviour
    {
        [SerializeField] GameObject mover;
        private bool isDead = false;

        public LayerMask floorLayer;
        [SerializeField] private float raycastLength;
        private int rayCount = 0;
        private BoxCollider2D activeHitbox;
        private GameObject tile;
        private GameObject tile1;
        private GameObject tile2;
        private GameObject tile3;
        [SerializeField] Sprite bloodFloor;

        private Animator anim;
        private Vector2 oldTrans;
        private Vector2 currentTrans;
        private float xBet;
        private float yBet;
        private float transX;
        private float transY;
        // Start is called before the first frame update
        void Start()
        {
            activeHitbox = this.GetComponent<BoxCollider2D>();
            anim = this.GetComponent<Animator>();
            oldTrans = this.transform.position;
        }

        private void Update()
        {
            currentTrans = this.transform.position;
            transX = this.transform.position.x;
            transY = this.transform.position.y;
            xBet = Mathf.Abs(oldTrans.x) - Mathf.Abs(currentTrans.x);
            yBet = Mathf.Abs(oldTrans.y) - Mathf.Abs(currentTrans.y);

            if (Mathf.Abs(xBet)> Mathf.Abs(yBet)&&transX>0)//left or right
            {
                anim.SetBool("isSide", true);
                anim.SetBool("isUp", false);
                anim.SetBool("isDown", false);
                if (xBet<0)//going left
                {
                    this.GetComponent<SpriteRenderer>().flipX = false;//started true
                }
                else//going right
                {
                    this.GetComponent<SpriteRenderer>().flipX = true;//started false
                }
            }
            else if (Mathf.Abs(xBet) < Mathf.Abs(yBet)&&transY>0)//up or down
            {
                anim.SetBool("isSide", false);
                if (yBet>0)
                {
                    anim.SetBool("isUp", false);//started true
                    anim.SetBool("isDown", true);
                }
                else
                {
                    anim.SetBool("isUp", true);//started false
                    anim.SetBool("isDown",false);
                }
            }
            else if (Mathf.Abs(xBet) > Mathf.Abs(yBet) && transX <= 0)
            {
                anim.SetBool("isSide", true);
                anim.SetBool("isUp", false);
                anim.SetBool("isDown", false);
                if (xBet < 0)//going left
                {
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }
                else//going right
                {
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            else if (Mathf.Abs(xBet) < Mathf.Abs(yBet) && transY <= 0)
            {
                anim.SetBool("isSide", false);
                if (yBet > 0)
                {
                    anim.SetBool("isUp", true);
                    anim.SetBool("isDown", false);
                }
                else
                {
                    anim.SetBool("isUp", false);
                    anim.SetBool("isDown", true);
                }
            }
            
            oldTrans = this.transform.position;
        }

        void FixedUpdate()
        {
        if (rayCount <4 && isDead) {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x, transform.position.y-.3f), Vector2.down, raycastLength, floorLayer);// Cast a ray straight down.
            RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x, transform.position.y+.3f), Vector2.up, raycastLength, floorLayer);// Cast a ray straight up.
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x-.3f, transform.position.y), Vector2.left, raycastLength, floorLayer);// Cast a ray straight left.
            RaycastHit2D hit3 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x+.3f, transform.position.y), Vector2.right, raycastLength, floorLayer);// Cast a ray straight right.
            Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.down * raycastLength);
            Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.up * raycastLength);
            Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.left * raycastLength);
            Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.right * raycastLength);

            // If it hits something...
            if (hit.collider == null && hit1.collider == null && hit2.collider == null && hit3.collider == null)
            {
                Debug.Log("NPC not hitting something");

            }
            else if ((hit.collider != null && hit.collider.gameObject.CompareTag("Floortile")) || (hit1.collider != null && hit1.collider.gameObject.CompareTag("Floortile")) || (hit2.collider != null && hit2.collider.gameObject.CompareTag("Floortile")) || (hit3.collider != null && hit3.collider.gameObject.CompareTag("Floortile")))
            {
                if (hit.collider.gameObject.CompareTag("Floortile"))
                { 
                    tile = hit.collider.gameObject;
                    Debug.Log("tile is hitting NPC");
                    if (isDead)
                    {
                        Debug.Log("changed floor " + this.name + " to blood tile");
                        tile.GetComponent<SpriteRenderer>().sprite = bloodFloor;
                    }
                }
                if (hit1.collider.gameObject.CompareTag("Floortile")) 
                { 
                    tile1 = hit1.collider.gameObject;
                    Debug.Log("tile is hitting NPC");
                    if (isDead)
                    {
                        Debug.Log("changed floor " + this.name + " to blood tile");
                        tile1.GetComponent<SpriteRenderer>().sprite = bloodFloor;
                    }
                }
                if (hit2.collider.gameObject.CompareTag("Floortile")) 
                { 
                    tile2 = hit2.collider.gameObject;
                    Debug.Log("tile is hitting NPC");
                    if (isDead)
                    {
                        Debug.Log("changed floor " + this.name + " to blood tile");
                        tile2.GetComponent<SpriteRenderer>().sprite = bloodFloor;
                    }
                }
                if (hit3.collider.gameObject.CompareTag("Floortile")) 
                { 
                    tile3 = hit3.collider.gameObject;
                    Debug.Log("tile is hitting NPC");
                    if (isDead)
                    {
                        Debug.Log("changed floor " + this.name + " to blood tile");
                        tile3.GetComponent<SpriteRenderer>().sprite = bloodFloor;
                    }
                }
                
            }
                rayCount++;
        }
        }

        public void StopMovement()
        {
            PathFollowerUtility.StopFollowing(mover.transform);
            mover.SetActive(false);
            Destroy(this.GetComponent<QPathFinder.PathFollowerToPosition>());
        }

        public void SetDead(bool newD)
        {
            isDead = newD;
            this.GetComponent<AudioSource>().enabled = false;
        }

        public bool GetDead()
        {
            return isDead;
        }
    }
}
