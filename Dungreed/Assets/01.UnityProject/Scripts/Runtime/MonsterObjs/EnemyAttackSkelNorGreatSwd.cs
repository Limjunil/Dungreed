using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAttackSkelNorGreatSwd : CreateEnemyBullet
{

    public EnemySkelNorGreatSwd enemySkel = default;

    public Animator skelAnimator = default;

    public BoxCollider2D swordcollider = default;

    public bool isChkAttack = false;

    public bool isDieSkel = false;

    // Start is called before the first frame update
    void Start()
    {
        isDieSkel = false;
        isChkAttack = false;
        this.spawnFireBullet = 3f;

        enemySkel = gameObject.GetComponentInParent<EnemySkelNorGreatSwd>();


        skelAnimator = gameObject.GetComponentInParent<Animator>();

        GameObject skelSword = gameObject.FindChildObj("GreatSwordSkel");
        swordcollider = skelSword.GetComponentMust<BoxCollider2D>();

        swordcollider.enabled = false;

        this.timeAfterSpawn = 0;

        this.target = default;
        this.targetPlayer = default;

        target = GameObject.FindWithTag("Player");
        targetPlayer = target.transform;

    }

    // Update is called once per frame
    void Update()
    {
        this.timeAfterSpawn += Time.deltaTime;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (isDieSkel == true) { return; }
            enemySkel.OnFindPlayer();

            Vector2 len = targetPlayer.position - gameObject.transform.position;

            float lenIsMagnitud = len.magnitude;

            if (spawnFireBullet <= timeAfterSpawn && lenIsMagnitud < 1f
                && isChkAttack == false)
            {
                isChkAttack = true;
                swordcollider.enabled = true;

                skelAnimator.SetTrigger("AttackSkel");

                StartCoroutine(OffAttackSkel());
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (isDieSkel == true) { return; }
            enemySkel.OnDisappearPlayer();
        }
    }

    public void OffBoxCollider()
    {
        swordcollider.enabled = false;
    }

    public void WaitAttack()
    {
        isChkAttack = false;
    }

    IEnumerator OffAttackSkel()
    {

        yield return new WaitForSeconds(4f);
        OffBoxCollider();

        yield return new WaitForSeconds(3f);

        WaitAttack();
    }

    public void OnSkelNorDie()
    {
        isDieSkel = true;
    }


}
