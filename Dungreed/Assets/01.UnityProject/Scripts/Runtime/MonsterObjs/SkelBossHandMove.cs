using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkelBossHandMove : MonoBehaviour
{
    public Animator skelBossLtHandAni = default;
    public Rigidbody2D skelBossHandRigid = default;
    public GameObject target = default;
    public Transform targetPlayer = default;

    public SkelBossController skelBossController = default;

    public float skelBossHandSpeed = default;

    private bool isLaserPattern = false;

    public float countTime = default;

    public bool laserOne = false;

    public bool OnlyOnePlz = false;



    // Start is called before the first frame update
    void Start()
    {
        OnlyOnePlz = false;
        laserOne = false;
        countTime = 0f;
        skelBossHandSpeed = 6f;
        isLaserPattern = false;
        skelBossHandRigid = gameObject.GetComponentMust<Rigidbody2D>();
        skelBossLtHandAni = gameObject.GetComponentMust<Animator>();

        skelBossController = gameObject.GetComponentInParent<SkelBossController>();
    }

    // Update is called once per frame
    void Update()
    {


        // 레이저 패턴이 끝나면 다시 대기 상태 애니메이션 활성화
        if (skelBossLtHandAni.GetCurrentAnimatorStateInfo(0).IsName("SkelBoss_Hand_Attack")
            && 0.99f <= skelBossLtHandAni.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            skelBossLtHandAni.SetTrigger("EndLaser");
        }

        // 레이저 패턴 시작
        if (isLaserPattern == true)
        {

            if (laserOne == false)
            {
                GoToPlayerPosHand();

                countTime += Time.deltaTime;

                if(1.5f < countTime)
                {
                    laserOne = true;
                    GFunc.Log("레이저 발사");
                    StartCoroutine(LaserPatternStart());
                }


            }
            
        }

        
    }

    //! 레이저 공격을 활성화 시키는 함수
    public void StartLaser()
    {
        isLaserPattern = true;
    }

    IEnumerator LaserPatternStart()
    {
                
        skelBossLtHandAni.SetTrigger("SkelBossLaser");

        yield return new WaitForSeconds(1f);

        isLaserPattern = false;
        laserOne = false;
        countTime = 0f;

        if(gameObject.transform.name == "SkelBossLt")
        {

            if(OnlyOnePlz == false)
            {
                OnlyOnePlz = true;
                skelBossController.OnLaserOnePattern();
            }
            else
            {
                OnlyOnePlz = false;

            }

        }

    }

    public void GoToPlayerPosHand()
    {
        target = GameObject.FindWithTag("Player");
        targetPlayer = target.transform;


        Vector2 goToPlayerPos = new Vector2(gameObject.transform.position.x,
            targetPlayer.position.y);

        transform.position = Vector2.MoveTowards(gameObject.transform.position,
            goToPlayerPos, skelBossHandSpeed * Time.deltaTime);
    }

}
