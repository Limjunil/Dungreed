using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject bullet;
    public Transform pos;
    public float cooltime;
    private float curtime;
    GameObject damagePos;

    // Start is called before the first frame update
    void Start()
    {
        damagePos = gameObject.FindChildObj("DamagePos");
    }

    // Update is called once per frame
    void Update()

    {

        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, z);



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
