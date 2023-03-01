using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkelBossController : MonoBehaviour
{
    //public Animator skelBossLtHandAni = default;
    private SkelBossHandMove skelBossLtHandMove = default;
    private SkelBossHandMove skelBossRtHandMove = default;
    private SkelBossBulletPattern skelBossBullet = default;
    private SkelBossSwordPattern skelBossSwd = default;
    private GameObject monsterhpBack = default;
    private GameObject monsterHpBar = default;
    private Image monsterHp = default;
    private GameObject skelBossDead = default;
    private Animator skelBossAni = default;
    private BoxCollider2D skelBossHeadCollider = default;


    public float enemyAmount = default;
    public int enemyMaxHp = default;
    public int enemyCurrentHp = default;
    public bool isLaserOne = false;
    public bool isLaserTwo = false;
    public bool isDieSkelBoss = false;
    public bool isFirstPattern = false;

    public BgAudioManager bgAudioManager = default;

    public EnemyObjs enemyObjs = default;

    // Start is called before the first frame update
    void Start()
    {
        isFirstPattern = false;
        isDieSkelBoss = false;
        isLaserOne = false;
        isLaserTwo = false;

        GameObject skelBossLtHand_ = gameObject.FindChildObj("SkelBossLt");
        GameObject skelBossRtHand_ = gameObject.FindChildObj("SkelBossRt");

        skelBossLtHandMove = skelBossLtHand_.GetComponentMust<SkelBossHandMove>();
        skelBossRtHandMove = skelBossRtHand_.GetComponentMust<SkelBossHandMove>();
        
        GameObject skelBossBack_ = gameObject.FindChildObj("SkelBossBack");
        GameObject skelBossBullets_ = gameObject.FindChildObj("SkelBossBullets");
        GameObject skelBossSword_ = gameObject.FindChildObj("SkelBossSwds");
        bgAudioManager = GFunc.GetRootObj("BgAudioManager").GetComponentMust<BgAudioManager>();



        skelBossBullet = skelBossBullets_.GetComponentMust<SkelBossBulletPattern>();
        skelBossSwd = skelBossSword_.GetComponentMust<SkelBossSwordPattern>();
        

        enemyObjs = new SkelBoss();
        enemyMaxHp = enemyObjs.MonsterHp();
        enemyCurrentHp = enemyObjs.MonsterHp();

        GetBossHpComonet();

        skelBossDead = gameObject.FindChildObj("SkelBossDead");
        skelBossDead.SetActive(false);

        skelBossAni = gameObject.GetComponentMust<Animator>();
        skelBossHeadCollider = gameObject.GetComponentMust<BoxCollider2D>();
        skelBossHeadCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterBossHpVal();

        if (isDieSkelBoss == true) { return; }


        if (isFirstPattern == false)
        {
           
            StartCoroutine(SkelBossPattern());

            isFirstPattern = true;

        }

        if(isLaserOne == true)
        {
            StartCoroutine(LaserPatternTwo());

            isLaserOne = false;

        }

    }

    IEnumerator LaserPatternTwo()
    {
        skelBossRtHandMove.StartLaser();

        yield return new WaitForSeconds(2f);

        skelBossLtHandMove.StartLaser();

    }

    public void OnLaserOnePattern()
    {
        isLaserOne = true;
    }


    //! 보스의 hp 바를 가져오는 함수
    public void GetBossHpComonet()
    {

        GameObject uiObjs_ = GFunc.GetRootObj("UiObjs");

        monsterhpBack = uiObjs_.FindChildObj("MonsterHpBack");
        monsterHpBar = monsterhpBack.FindChildObj("MonsterHpBar");

        monsterHp = monsterHpBar.GetComponentMust<Image>();


    }

    //실시간 HP 보여주기
    public void MonsterBossHpVal()
    {
        enemyAmount = enemyCurrentHp / (float)enemyMaxHp;
        monsterHp.fillAmount = enemyAmount;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sword" || collision.tag == "DashAttack")
        {
            enemyCurrentHp -= 8;

            if (enemyCurrentHp <= 0)
            {
                StartCoroutine(MonsterBossDie());
            }
        }
    }

    IEnumerator MonsterBossDie()
    {
        // 보스 처치시 발생

        isDieSkelBoss = true;
        skelBossHeadCollider.enabled = false;
        monsterhpBack.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

        skelBossAni.SetTrigger("DieSkelBoss");

        yield return new WaitForSeconds(0.5f);

        bgAudioManager.EndBossBgSound();


    }


    IEnumerator SkelBossPattern()
    {

        yield return new WaitForSeconds(1f);

        while (0 < enemyCurrentHp)
        {
            int randamVal_ = Random.Range(1, 3 + 1);

            switch (randamVal_)
            {
                case 1:
                    skelBossLtHandMove.StartLaser();
                    break;
                case 2:
                    skelBossBullet.OnStartBulletPattern();
                    break;
                case 3:
                    skelBossSwd.OnSkelSwdPattern();
                    break;
            }

            yield return new WaitForSeconds(9f);
        }
    }
}
