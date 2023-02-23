using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBossController : MonoBehaviour
{
    public Animator skelBossLtHandAni = default;

    // Start is called before the first frame update
    void Start()
    {

        GameObject skelBossLtHand_ = gameObject.FindChildObj("SkelBossLt");
        skelBossLtHandAni = skelBossLtHand_.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            skelBossLtHandAni.SetTrigger("SkelBossLaser");
        }

        if(skelBossLtHandAni.GetCurrentAnimatorStateInfo(0).IsName("SkelBoss_Hand_Attack")
            && 0.99f <= skelBossLtHandAni.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            skelBossLtHandAni.SetTrigger("EndLaser");
        }
    }
}
