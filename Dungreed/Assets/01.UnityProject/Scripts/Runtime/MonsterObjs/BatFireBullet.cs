using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFireBullet : MonoBehaviour
{

    private Rigidbody2D batFireRigid = default;

    public float batFireSpeed = default;

    public Animator animator = default;

    // Start is called before the first frame update
    void Start()
    {
        batFireRigid = gameObject.GetComponentMust<Rigidbody2D>();
        batFireSpeed = 3f;
        animator = gameObject.GetComponentMust<Animator>();

        batFireRigid.velocity = transform.up * batFireSpeed;
    }
    void OnEnable()
    {
        StartCoroutine("StartAutoOffBullet");

    }

    // Update is called once per frame
    void Update()
    {
        

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StopCoroutine("StartAutoOffBullet");
            StartCoroutine(OffBullet());
        }
    }

    IEnumerator StartAutoOffBullet()
    {
        yield return new WaitForSeconds(4f);
        
        StartCoroutine(OffBullet());

    }


    IEnumerator OffBullet()
    {
        batFireRigid.velocity = Vector2.zero;

        animator.SetTrigger("EndFire");
        GFunc.Log("실행됨");
        yield return new WaitForSeconds(0.7f);

        gameObject.SetActive(false);
    }
}
