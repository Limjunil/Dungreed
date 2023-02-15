using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFireBullet : MonoBehaviour
{

    private Rigidbody2D batFireRigid = default;

    public float batFireSpeed = default;


    // Start is called before the first frame update
    void Start()
    {
        batFireRigid = gameObject.GetComponentMust<Rigidbody2D>();
        batFireSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        batFireRigid.velocity = transform.up * batFireSpeed;

    }
}
