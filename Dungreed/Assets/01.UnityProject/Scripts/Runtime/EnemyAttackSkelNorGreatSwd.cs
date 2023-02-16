using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSkelNorGreatSwd : CreateEnemyBullet
{

    public GameObject greatSwordSkel = default;

    private BoxCollider2D skelCollider = default;

    private Animator skelAnimator = default;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnFireBullet = 3f;
        greatSwordSkel = gameObject.FindChildObj("GreatSwordSkel");

        skelCollider = greatSwordSkel.GetComponentMust<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        this.timeAfterSpawn += Time.deltaTime;
    }

    //public void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        if (spawnFireBullet <= timeAfterSpawn)
    //        {
    //            skelCollider.enabled = true;

    //            skelAnimator.SetTrigger("AttackSkel");


    //        }
    //    }
    //}
}
