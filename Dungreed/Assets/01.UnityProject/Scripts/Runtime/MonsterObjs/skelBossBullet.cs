using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBossBullet : MonoBehaviour
{

    private Rigidbody2D skelBossBulletRigid = default;

    public float bulletSpeed = default;

    // Start is called before the first frame update
    void Start()
    {
        skelBossBulletRigid = gameObject.GetComponentMust<Rigidbody2D>();
        bulletSpeed = 5f;
        
    }

    void OnEnable()
    {
        StartCoroutine("StartAutoOffBullet");

    }

    // Update is called once per frame
    void Update()
    {

        skelBossBulletRigid.velocity = transform.up * bulletSpeed;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Player")
        //{
        //    StopCoroutine("StartAutoOffBullet");
        //    StartCoroutine(OffBullet());
        //}
    }

    IEnumerator StartAutoOffBullet()
    {
        yield return new WaitForSeconds(4f);

        StartCoroutine(OffBullet());

    }


    IEnumerator OffBullet()
    {
        skelBossBulletRigid.velocity = Vector2.zero;

        //animator.SetTrigger("EndFire");
        GFunc.Log("실행됨");
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
