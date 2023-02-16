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

        Invoke("OffBullet", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        batFireRigid.velocity = transform.up * batFireSpeed;

    }

    public void OffBullet()
    {
        animator.SetTrigger("EndFire");
        gameObject.SetActive(false);
    }
}
