using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EnemyBatRed : EnemyMoveController
{
    public GameObject dieBatRedImage = default;
    public Image batRedImage = default;
    public EnemyAttackBatRed attackBatRed = default;

    // Start is called before the first frame update
    void Start()
    {
        dieBatRedImage = gameObject.FindChildObj("DieBatRedImage");
        dieBatRedImage.SetActive(false);

        enemyObjs = new BatRed();
        isDie = false;

        this.enemyMaxHp = enemyObjs.MonsterHp();
        this.enemyCurrentHp = enemyObjs.MonsterHp();

        this.enemySpeed = enemyObjs.MonsterSpeed();
        this.changeWay = default;
        this.enemyRigid = gameObject.GetComponentMust<Rigidbody2D>();
        this.target = default;
        this.targetPlayer = default;

        this.isFindPlayer = false;
        this.enemyFly = enemyObjs.MonsterCanFly();

        GameObject searchPlayer = gameObject.FindChildObj("SearchPlayer");

        attackBatRed = searchPlayer.GetComponentMust<EnemyAttackBatRed>();

        batRedImage = gameObject.GetComponentMust<Image>();


        GetHpBarComonet();
        GetSteleControl();
        GetPlayerController();

        Invoke("RandomWay", 1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (isDie == true)
        {
            return;
        }
        MonsterHpVal();

        OnMoveEnemy();
    }
    public override void GetPlayerController()
    {
        base.GetPlayerController();
    }

    public override void SendPlayerExp()
    {
        base.SendPlayerExp();
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
        if (collision.tag == "Sword" || collision.tag == "DashAttack")
        {
            monsterhpBack.transform.localScale = Vector3.one;
            enemyCurrentHp -= 8;

            if (enemyCurrentHp <= 0)
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
            else
            {
                enemyRigid.velocity = new Vector2(-1f * enemyRigid.velocity.x,
                -1f * enemyRigid.velocity.y);
            }

        }

    }



    public override void MonsterDie()
    {
        base.MonsterDie();
                
        batRedImage.enabled = false;

        dieBatRedImage.SetActive(true);
        attackBatRed.OnBatRedDie();
        enemyRigid.gravityScale = 1f;
        SendPlayerExp();

        StartCoroutine(OffBatRed());
    }


    IEnumerator OffBatRed()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

}
