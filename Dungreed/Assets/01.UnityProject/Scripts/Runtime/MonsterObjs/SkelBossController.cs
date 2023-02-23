using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBossController : MonoBehaviour
{
    //public Animator skelBossLtHandAni = default;
    private SkelBossHandMove skelBossLtHandMove = default;
    private SkelBossHandMove skelBossRtHandMove = default;
    private SkelBossBulletPattern skelBossBullet = default;

    public bool isLaserOne = false;
    public bool isLaserTwo = false;


    // Start is called before the first frame update
    void Start()
    {
        isLaserOne = false;
        isLaserTwo = false;

        GameObject skelBossLtHand_ = gameObject.FindChildObj("SkelBossLt");
        GameObject skelBossRtHand_ = gameObject.FindChildObj("SkelBossRt");

        skelBossLtHandMove = skelBossLtHand_.GetComponentMust<SkelBossHandMove>();
        skelBossRtHandMove = skelBossRtHand_.GetComponentMust<SkelBossHandMove>();
        
        GameObject skelBossBack_ = gameObject.FindChildObj("SkelBossBack");
        GameObject skelBossBullets_ = gameObject.FindChildObj("SkelBossBullets");


        skelBossBullet = skelBossBullets_.GetComponentMust<SkelBossBulletPattern>();
        //skelBossLtHandAni = skelBossLtHand_.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //skelBossLtHandMove.StartLaser();
            skelBossBullet.OnStartBulletPattern();

        }

        if(isLaserOne == true)
        {
            int randomVal = Random.Range(-1, 1 + 1);

            if(0 <= randomVal)
            {
                skelBossRtHandMove.StartLaser();
            }

            isLaserOne = false;

        }

        if (isLaserTwo == true)
        {
            skelBossLtHandMove.StartLaser();
            isLaserTwo = false;

        }
        //if(skelBossLtHandAni.GetCurrentAnimatorStateInfo(0).IsName("SkelBoss_Hand_Attack")
        //    && 0.99f <= skelBossLtHandAni.GetCurrentAnimatorStateInfo(0).normalizedTime)
        //{
        //    skelBossLtHandAni.SetTrigger("EndLaser");
        //}
    }

    public void OnLaserOnePattern()
    {
        isLaserOne = true;
    }

    public void OnLaserTwoPattern()
    {
        isLaserTwo = true;
    }
}
