using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrickAI : MonoBehaviour
{
    private float rand;//pick random amount of time to block path
    private Animator anim;
    private float timer = 0f;
    [SerializeField] float blockTime;
    [SerializeField] float waitTime;
    private AudioSource audioSrs;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rand = Random.Range(0,1);
        audioSrs = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= blockTime)
        {
            StartCoroutine(BlockPath());
            timer = 0f;
            Debug.Log("timer finished");
        }
    }

    IEnumerator BlockPath()
    {
        anim.SetBool("closeOff", true);
        audioSrs.Play();
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        audioSrs.Play();
        anim.SetBool("closeOff", false);
    }
}
