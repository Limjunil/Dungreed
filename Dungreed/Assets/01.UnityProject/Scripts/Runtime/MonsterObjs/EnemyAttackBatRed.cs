using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackBatRed : CreateEnemyBullet
{

    public EnemyBatRed enemyBatRed = default;

    public bool isBatRedDie = false;

    public GameObject dungObjs = default;

    // Start is called before the first frame update
    void Start()
    {
        isBatRedDie = false;
        this.bulletCnt = 20;
        this.bullet = new GameObject[bulletCnt];

        this.spawnFireBullet = 3f;
        this.batFirePrefab = Resources.Load<GameObject>("Prefabs/BatFire");


        GameObject batRed_ = gameObject.transform.parent.gameObject;
        enemyBatRed = batRed_.GetComponentMust<EnemyBatRed>();

        dungObjs = batRed_.transform.parent.gameObject;

        Vector3 BatBulletFirstPos = new Vector3(-1000f, 0f, 0f);

        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i] = Instantiate(batFirePrefab, BatBulletFirstPos,
                Quaternion.identity, dungObjs.transform);

            bullet[i].transform.localScale = new Vector3(0.283f, 0.283f, 0.283f);

            bullet[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.timeAfterSpawn += Time.deltaTime;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isBatRedDie == true) { return; }

            if (spawnFireBullet <= timeAfterSpawn)
            {
                enemyBatRed.OnFindPlayer();


                target = GameObject.FindWithTag("Player");
                targetPlayer = target.transform;



                bullet[bulletCnt - 1].SetActive(true);
                timeAfterSpawn = 0;

                bullet[bulletCnt - 1].transform.position = gameObject.transform.position;

                Vector2 direction = new Vector2(
                    transform.position.x - targetPlayer.position.x,
                    transform.position.y - targetPlayer.position.y);

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;


                Quaternion angleAxis = Quaternion.AngleAxis(
                    angle - 90f, Vector3.forward);

                Quaternion rotation = Quaternion.Slerp(transform.rotation,
                    angleAxis, 100f * Time.deltaTime);

                bullet[bulletCnt - 1].transform.rotation = rotation;

                bulletCnt--;

                if (bulletCnt == 1)
                {
                    bulletCnt = 20;
                }
            }
        }
    }   // OnTriggerStay2D()

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isBatRedDie == true) { return; }

            enemyBatRed.OnDisappearPlayer();
        }
    }


    //! 붉은 박쥐가 죽을 때 쓰는 함수
    public void OnBatRedDie()
    {
        isBatRedDie = true;
    }


    

}
