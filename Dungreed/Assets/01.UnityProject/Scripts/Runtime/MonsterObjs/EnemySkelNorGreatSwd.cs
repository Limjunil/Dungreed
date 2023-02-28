using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySkelNorGreatSwd : EnemyMoveController
{

    public EnemyObjs enemyObjs = default;
    public Animator skelAnimator = default;

    public MonsterCollider monsterCollider = default;


    public EnemyAttackSkelNorGreatSwd attackSkelNor = default;

    // Start is called before the first frame update
    void Start()
    {
        enemyObjs = new SkelNorGreatSwd();
        skelAnimator = gameObject.GetComponentMust<Animator>();
        isDie = false;
        monsterCollider = gameObject.GetComponentMust<MonsterCollider>();


        this.enemyMaxHp = enemyObjs.MonsterHp();
        this.enemyCurrentHp = enemyObjs.MonsterHp();

        this.enemySpeed = enemyObjs.MonsterSpeed();
        this.changeWay = default;
        this.enemyRigid = gameObject.GetComponentMust<Rigidbody2D>();
        this.target = default;
        this.targetPlayer = default;

        this.isFindPlayer = false;
        this.enemyFly = enemyObjs.MonsterCanFly();

        GameObject rotateWeaSkel = gameObject.FindChildObj("RotateWeaSkel");

        attackSkelNor = rotateWeaSkel.GetComponentMust<EnemyAttackSkelNorGreatSwd>();

        GetHpBarComonet();
        GetSteleControl();

        Invoke("RandomWay", 1f);


    }

    // Update is called once per frame
    void Update()
    {

        if(isDie == true)
        {
            return;
        }

        MonsterHpVal();

        OnMoveEnemy();

    }


    public override void RandomWay()
    {
        base.RandomWay();
    }

    public override void OnMoveEnemy()
    {
        base.OnMoveEnemy();
    }

    public override void DiscoveryPlayer()
    {
        base.DiscoveryPlayer();
    }

    public override void OnDisappearPlayer()
    {
        base.OnDisappearPlayer();
    }

    public override void OnFindPlayer()
    {
        base.OnFindPlayer();
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sword" || collision.tag == "DashAttack")
        {
            monsterhpBack.transform.localScale = Vector3.one;
            enemyCurrentHp -= 8;

            if(enemyCurrentHp <= 0)
            {
                DieSendStele();
                MonsterDie();
            }

        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")) ||
            collision.gameObject.layer.Equals(LayerMask.NameToLayer("GroundBg")))
        {
            if (isDie == true)
            {
                GFunc.Log("실행봄");
                enemyRigid.constraints = RigidbodyConstraints2D.FreezePositionY;

            }

        }
    }

    public override void MonsterDie()
    {
        base.MonsterDie();

        attackSkelNor.OnSkelNorDie();
        skelAnimator.SetTrigger("DieSkel");

        monsterCollider.SkelDie(true, 1f);


        StartCoroutine(OffSkelNor());
    }

    IEnumerator OffSkelNor()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
