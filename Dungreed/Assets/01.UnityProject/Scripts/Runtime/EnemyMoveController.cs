using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{


    protected float enemySpeed = default;

    protected int changeWay = default;

    protected Rigidbody2D enemyRigid = default;
    protected GameObject target = default;
    protected Transform targetPlayer = default;

    protected bool isFindPlayer = false;
    protected bool enemyFly = false;



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
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);

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
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            enemyRigid.velocity = new Vector2(1, enemyRigid.velocity.y);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            enemyRigid.velocity = new Vector2(-1, enemyRigid.velocity.y);

        }

        float lenIsMagnitud = len.magnitude;


        if (enemyFly == true)
        {
            if (lenIsMagnitud < 3f)
            {
                enemyRigid.velocity = Vector2.zero;
            }
        }
        else
        {
            if (lenIsMagnitud < 1f)
            {
                enemyRigid.velocity = Vector2.zero;
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


}
