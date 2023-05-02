using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject richardMain;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health<=0)
        {
            if (gameObject.CompareTag("Richard"))
            {
                Destroy(richardMain);
            }
            Destroy(gameObject);
        }
    }

    public void DecreaseHealth(int damage)
    {
        StartCoroutine(AnimPlay(damage));
    }

    IEnumerator AnimPlay(int damage)
    {
        if (gameObject.GetComponent<BoxCollider2D>().enabled) {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        anim.Play("MonsterHit");
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Just finished waiting");
        health -= damage;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
