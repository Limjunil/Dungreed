using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EnemyMoveController : MonoBehaviour
{

    protected int enemyMaxHp = default;
    protected int enemyCurrentHp = default;
    protected float enemyAmount = default;
    protected Image monsterHp = default;
    protected GameObject monsterhpBack = default;
    protected GameObject monsterHpBar = default;
    protected Canvas monsterHpSort = default;
    protected bool isDie = default;


    protected float enemySpeed = default;

    protected int changeWay = default;

    protected Rigidbody2D enemyRigid = default;
    protected GameObject target = default;
    protected Transform targetPlayer = default;

    protected bool isFindPlayer = false;
    protected bool enemyFly = false;

    protected bool enemyUpDown = false;


    public virtual void RandomWay()
    {
        // 좌우 랜덤으로 이동
        this.changeWay = Random.Range(-1, 1 + 1);

        Invoke("RandomWay", 1f);

    }   // RandomWay()

    public virtual void OnMoveEnemy()
    {
        if (isFindPlayer == true)
        {
            DiscoveryPlayer();
            return;
        }


        enemyRigid.velocity = new Vector2(changeWay, enemyRigid.velocity.y);

        if (0 < changeWay)
        {
            gameObject.transform.localScale = new Vector3(0.283f, 0.283f, 0.283f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-0.283f, 0.283f, 0.283f);

        }
    }   // OnMoveEnemy()

    public virtual void DiscoveryPlayer()
    {
        target = GameObject.FindWithTag("Player");
        targetPlayer = target.transform;

        Vector2 len = targetPlayer.position - gameObject.transform.position;

        float lookZ = Mathf.Atan2(len.y, len.x);

        if (-1.5f < lookZ && lookZ < 1.5f)
        {
            gameObject.transform.localScale = new Vector3(0.283f, 0.283f, 0.283f);
            
            if (enemyFly == false)
            {

                enemyRigid.velocity = new Vector2(1, enemyRigid.velocity.y);
            }
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-0.283f, 0.283f, 0.283f);

            if (enemyFly == false)
            {
                enemyRigid.velocity = new Vector2(-1, enemyRigid.velocity.y);

            }

        }

        float lenIsMagnitud = len.magnitude;

        if (enemyFly == true)
        {
            if (lenIsMagnitud < 3f)
            {
                enemyRigid.velocity = Vector2.zero;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position,
                enemySpeed * Time.deltaTime);
            }
        }
        else
        {
            if (lenIsMagnitud < 1f)
            {
                enemyRigid.velocity = Vector2.zero;
            }
            else
            {
                
                if (targetPlayer.position.y < gameObject.transform.position.y
                    && lenIsMagnitud < 4f)
                {
                    if (enemyUpDown == false)
                    {
                        enemyRigid.velocity = Vector2.down;
                        enemyUpDown = true;
                        StartCoroutine(OffUpDown());
                    }
                }
                else if (gameObject.transform.position.y + 0.06< targetPlayer.position.y
                    && lenIsMagnitud < 4f)
                {
                    if (enemyUpDown == false)
                    {
                        GFunc.Log($"해골의 y 포지션{gameObject.transform.position.y} + " +
                            $"플레이어의 y 포지션 {targetPlayer.position.y}");
                        GFunc.Log("해골 점프");
                        enemyRigid.velocity = Vector2.up * enemySpeed * 8f;

                        enemyUpDown = true;
                        StartCoroutine(OffUpDown());
                    }

                }


            }
        }


    }   // DiscoveryPlayer()

    public virtual void OnFindPlayer()
    {
        isFindPlayer = true;
    }

    public virtual void OnDisappearPlayer()
    {
        isFindPlayer = false;

    }

    IEnumerator OffUpDown()
    {
        yield return new WaitForSeconds(2f);
        enemyUpDown = false;
    }


    //! hp 바를 가져오는 함수
    public virtual void GetHpBarComonet()
    {
        monsterhpBack = gameObject.FindChildObj("MonsterHpBack");
        monsterHpBar = monsterhpBack.FindChildObj("MonsterHpBar");

        monsterHp = monsterHpBar.GetComponentMust<Image>();
        monsterHpSort = monsterhpBack.GetComponentMust<Canvas>();

        monsterHpSort.sortingLayerName = "Ground";

        monsterhpBack.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

    }

    //실시간 HP 보여주기
    public virtual void MonsterHpVal()
    {
        enemyAmount = enemyCurrentHp / (float)enemyMaxHp;
        monsterHp.fillAmount = enemyAmount;
    }


    //! 몬스터가 죽으면 뜨는 함수
    public virtual void MonsterDie()
    {
        enemyRigid.velocity = Vector2.zero;

        monsterhpBack.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

        isDie = true;
    }

}
