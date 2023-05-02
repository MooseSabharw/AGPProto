using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPerson : MonoBehaviour
{
    private GameObject soundManager;
    public LayerMask collisionsLayer;
    [SerializeField] private float raycastLength;
    private BoxCollider2D activeHitbox;
    private GameObject NPC;
    private int animCount = 0;
    [SerializeField] GameObject attackVFX;
    [SerializeField] GameObject timer;
    public AnimationClip attackVFXClip;
    private Animator vfxAnim;
    private Animator anim;
    private PlayerMovement pmovement;


    void Start()
    {
        anim = GetComponent<Animator>();
        vfxAnim = attackVFX.GetComponent<Animator>();
        activeHitbox = this.GetComponent<BoxCollider2D>();
        pmovement = this.GetComponent<PlayerMovement>();

        soundManager = GameObject.Find("SoundManager");
    }


    void FixedUpdate()
    {
        //main
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.down, raycastLength, collisionsLayer);// Cast a ray straight down.
        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.up, raycastLength, collisionsLayer);// Cast a ray straight up.
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.left, raycastLength, collisionsLayer);// Cast a ray straight left.
        RaycastHit2D hit3 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.right, raycastLength, collisionsLayer);// Cast a ray straight right.
        //second
        RaycastHit2D hit4 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x + 1f, transform.position.y), Vector2.down, raycastLength, collisionsLayer);// Cast a ray straight down.
        RaycastHit2D hit5 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x - 1f, transform.position.y), Vector2.up, raycastLength, collisionsLayer);// Cast a ray straight up.
        RaycastHit2D hit6 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x, transform.position.y + 1f), Vector2.left, raycastLength, collisionsLayer);// Cast a ray straight left.
        RaycastHit2D hit7 = Physics2D.Raycast(new Vector2(activeHitbox.bounds.center.x, transform.position.y - 1f), Vector2.right, raycastLength, collisionsLayer);// Cast a ray straight right.

        Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.down * raycastLength);
        Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.up * raycastLength);
        Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.left * raycastLength);
        Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x, transform.position.y), Vector2.right * raycastLength);

        Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x+1f, transform.position.y+1.75f), Vector2.down * raycastLength);
        Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x-1f, transform.position.y-1.75f), Vector2.up * raycastLength);
        Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x+1.75f, transform.position.y+1f), Vector2.left * raycastLength);
        Debug.DrawRay(new Vector2(activeHitbox.bounds.center.x-1.75f, transform.position.y-1f), Vector2.right * raycastLength);
        // If it hits something...
        if (hit.collider == null && hit1.collider == null && hit2.collider == null && hit3.collider == null && hit4.collider == null && hit5.collider == null && hit6.collider == null && hit7.collider == null)
        {
            Debug.Log("I'm not hitting something");
            
        }
        else
        {
            if (hit.collider.gameObject.CompareTag("NPC")){NPC = hit.collider.gameObject; }
            else if (hit1.collider.gameObject.CompareTag("NPC")){NPC = hit1.collider.gameObject;}
            else if (hit2.collider.gameObject.CompareTag("NPC")) { NPC = hit2.collider.gameObject; }
            else if (hit3.collider.gameObject.CompareTag("NPC")) { NPC = hit3.collider.gameObject; }
            else if (hit4.collider.gameObject.CompareTag("NPC")) { NPC = hit4.collider.gameObject; }
            else if (hit5.collider.gameObject.CompareTag("NPC")) { NPC = hit5.collider.gameObject; }
            else if (hit6.collider.gameObject.CompareTag("NPC")) { NPC = hit6.collider.gameObject; }
            else if (hit7.collider.gameObject.CompareTag("NPC")) { NPC = hit7.collider.gameObject; }
            
            Debug.Log("I'm hitting NPC");
            //HERE HERE HERE HERE
            //pmovement.SetPaused(0);//player movement not working HERE -- doesn't turn off: tried to enable actual component, but unable to enable again, tried setting bool and stopping movement by adding condition to input ifs
            if (NPC!=null) { NPC.GetComponent<QPathFinder.MovingAI>().StopMovement(); }
            if (animCount==0 && NPC != null && !NPC.GetComponent<QPathFinder.MovingAI>().GetDead()) {
                animCount++;
                Debug.Log("NPC started the if statement");
                StartCoroutine(AnimationTimer());
                Debug.Log("NPC before the playermovement enable");
                //pmovement.SetPaused(1);//player movement not working HERE
                Debug.Log("NPC after the playermovement enable");
            }
        }
        
    }

    private IEnumerator AnimationTimer()
    {
        Debug.Log("NPC Inside IEnumerator");
        pmovement.SetPaused(true);
        timer.GetComponent<Timer>().setPaused(false);
        soundManager.GetComponent<SoundManagerScript>().PlaySound("WolfSound");
        anim.SetTrigger("Attack");
        anim.SetBool("isMoving", false);
        anim.SetBool("Side", true);
        anim.SetBool("Up", false);
        Debug.Log("NPC After the first animation");
        vfxAnim.SetTrigger("Attack");
        Debug.Log("NPC Past the trigger");
        yield return new WaitForSeconds(1f);
        NPC.GetComponent<Animator>().SetBool("isDead", true);
        NPC.GetComponent<QPathFinder.MovingAI>().SetDead(true);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().IncreaseScore(-8);//decrease points for body being on the floor
        NPC.GetComponent<UnActiveSword>().enabled = true;
        NPC.tag = "Trash";//lookup if you can actually set an object's tag like this
        pmovement.SetPaused(false);
        timer.GetComponent<Timer>().setPaused(true);
        animCount = 0;
    }
}
