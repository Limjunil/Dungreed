using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject bullet;
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
        damagePos = gameObject.FindChildObj("DamagePos");
        weaponPos = gameObject.FindChildObj("WeaponPlay");
        isTurning = false;

        GameObject gameObjs = GFunc.GetRootObj("GameObjs");
        playerObj = gameObjs.FindChildObj("Player");
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

                GameObject swordBullet = Instantiate(bullet, bulletPos, transform.rotation, gameObject.transform);


                swordBullet.transform.Rotate(new Vector3(0, 0, -90));


            }

            curtime = cooltime;
        }

        curtime -= Time.deltaTime;
    }
}
