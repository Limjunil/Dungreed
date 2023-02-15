using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CreateEnemyBullet : MonoBehaviour
{

    public GameObject[] bullet;
    public GameObject batFirePrefab;

    public int bulletCnt = default;

    // 최근 생성 시점에서 지난 시간
    private float timeAfterSpawn = default;

    // 총알 생성 주기 : 초당 몇발
    public float spawnFireBullet = default;


    private GameObject target = default;
    private Transform targetPlayer = default;



    // Start is called before the first frame update
    void Start()
    {
        bulletCnt = 20;
        bullet = new GameObject[bulletCnt];
        spawnFireBullet = 3f;

        Vector3 BatBulletFirstPos = new Vector3(-1000f, 0f, 0f );

        for(int i = 0; i < bullet.Length; i++)
        {
            bullet[i] = Instantiate(batFirePrefab, BatBulletFirstPos,
                Quaternion.identity, gameObject.transform);

            bullet[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(spawnFireBullet <= timeAfterSpawn)
            {

                GFunc.Log("작동함");
                target = GameObject.FindWithTag("Player");
                targetPlayer = target.transform;


                GFunc.Log($"{targetPlayer.name}");

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

    }
}
