using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject swordNorbullet;

    public GameObject[] swordAttacks;

    public int swordCnt = default;

    public Transform pos;
    public float cooltime;
    public bool isTurning = false;

    private float curtime;
    GameObject damagePos;
    GameObject weaponPos;


    GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        swordCnt = 40;
        damagePos = gameObject.FindChildObj("DamagePos");
        weaponPos = gameObject.FindChildObj("WeaponPlay");
        isTurning = false;

        GameObject gameObjs = GFunc.GetRootObj("GameObjs");
        playerObj = gameObjs.FindChildObj("Player");

        swordAttacks = new GameObject[swordCnt];

        Vector3 saveBullet_ = new Vector3(-2000f, 0f, 0f);

        for(int i = 0; i < swordAttacks.Length; i++)
        {
            swordAttacks[i] = Instantiate(swordNorbullet, saveBullet_,
                Quaternion.identity, playerObj.transform);

            swordAttacks[i].SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()

    {

        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float lookZ = Mathf.Atan2(len.y, len.x);

        float z = lookZ * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0, 0, z);

        if (-1.5f < lookZ && lookZ < 1.5f)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            playerObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            
            gameObject.transform.localScale = new Vector3(-1f, -1f, 1f);

            playerObj.transform.localScale = new Vector3(-1f, 1f, 1f);

        }



        if (curtime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Vector3 bulletPos = damagePos.transform.position;

                swordAttacks[swordCnt-1].SetActive(true);
                swordAttacks[swordCnt-1].transform.position = bulletPos;

                swordAttacks[swordCnt - 1].transform.rotation = transform.rotation;

                if (-1.5f < lookZ && lookZ < 1.5f)
                {
                    swordAttacks[swordCnt - 1].transform.Rotate(new Vector3(0, 0, -90f));

                }
                else
                {
                    swordAttacks[swordCnt - 1].transform.Rotate(new Vector3(0, 0, 90f));

                }

                swordCnt--;


                //GameObject swordBullet = Instantiate(bullet, bulletPos, transform.rotation, gameObject.transform);


                //swordBullet.transform.Rotate(new Vector3(0, 0, -90));

                if(swordCnt == 1)
                {
                    swordCnt = 40;
                }

            }

            curtime = cooltime;
        }

        curtime -= Time.deltaTime;
    }


}
