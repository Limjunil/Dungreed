using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour
{

    public GameObject swordNorbullet = default;

    public GameObject[] swordAttacks = default;

    public int swordCnt = default;

    public float cooltime = default;

    private float curtime = default;

    public bool isAttack = false;

    public GameObject swordWeaponPlayObjs = default;
    public GameObject swordWeaponPlay2Objs = default;


    public GameObject swordDamagePos = default;


    public GameObject playerObj = default;

    public Canvas rotateSort = default;

    // 무기를 변경했는지 확인하는 bool 값 | false : 검, true : 쇠뇌
    public bool changeWeapon = false;

    public GameObject swordWeaObj = default;
    public GameObject bowWeaObj = default;


    public GameObject arrowProfab = default;
    public int arrowCount = default;
    public GameObject bowDamagePos = default;
    public GameObject[] bowAttack = default;
    public Animator bowAni = default;

    // Start is called before the first frame update
    void Start()
    {
        curtime = 0f;
        cooltime = 0.5f;
        changeWeapon = false;
        rotateSort = gameObject.GetComponentMust<Canvas>();

        rotateSort.sortingLayerName = "Player";

        isAttack = false;
        swordCnt = 40;

        // 검
        swordNorbullet = Resources.Load<GameObject>("Prefabs/SwordBasic");

        swordWeaObj = gameObject.FindChildObj("SwordWea");
        swordDamagePos = swordWeaObj.FindChildObj("DamagePos");
        swordWeaponPlayObjs = swordWeaObj.FindChildObj("WeaponPlay");
        swordWeaponPlay2Objs = swordWeaObj.FindChildObj("WeaponPlay2");
        swordWeaponPlay2Objs.SetActive(false);


        playerObj = gameObject.transform.parent.gameObject;

        GameObject gameObjs = GFunc.GetRootObj("GameObjs");


        swordAttacks = new GameObject[swordCnt];

        Vector3 saveBullet_ = new Vector3(-2000f, 0f, 0f);

        for(int i = 0; i < swordAttacks.Length; i++)
        {
            swordAttacks[i] = Instantiate(swordNorbullet, saveBullet_,
                Quaternion.identity, gameObjs.transform);

            swordAttacks[i].SetActive(false);
        }

        swordWeaObj.SetActive(true);


        // 쇠뇌
        arrowProfab = Resources.Load<GameObject>("Prefabs/Arrow");
        arrowCount = 40;
        bowAttack = new GameObject[arrowCount];

        bowWeaObj = gameObject.FindChildObj("BowWea");
        bowDamagePos = bowWeaObj.FindChildObj("DamagePos");

        GameObject bowObj_ = bowWeaObj.FindChildObj("BowWea");
        bowAni = bowObj_.GetComponentMust<Animator>();

        for (int i = 0; i < bowAttack.Length; i++)
        {
            bowAttack[i] = Instantiate(arrowProfab, saveBullet_,
                Quaternion.identity, gameObjs.transform);

            bowAttack[i].SetActive(false);
        }


        bowWeaObj.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // 마우스 휠 아래로 | 쇠뇌
            changeWeapon = true;
            swordWeaObj.SetActive(false);
            bowWeaObj.SetActive(true);
        }
        else if ( 0 < Input.GetAxis("Mouse ScrollWheel"))
        {
            // 마우스 휠 위로 | 검
            changeWeapon = false;
            swordWeaObj.SetActive(true);
            bowWeaObj.SetActive(false);

        }

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
            // 공격
            if (Input.GetMouseButtonDown(0))
            {
                if(isAttack == false)
                {
                    isAttack = true;

                    if(changeWeapon == false)
                    {
                        // 검
                        swordWeaponPlay2Objs.SetActive(true);
                        swordWeaponPlayObjs.SetActive(false);

                        rotateSort.sortingOrder = 3;
                    }
                    else
                    {
                        // 쇠뇌
                        rotateSort.sortingOrder = 3;
                    }
                }
                else
                {
                    isAttack = false;
                    if (changeWeapon == false)
                    {
                        swordWeaponPlay2Objs.SetActive(false);
                        swordWeaponPlayObjs.SetActive(true);
                        rotateSort.sortingOrder = 1;
                    }
                    else
                    {
                        rotateSort.sortingOrder = 3;
                    }

                }


                if(changeWeapon == false)
                {
                    // 검
                    Vector3 bulletPos = swordDamagePos.transform.position;

                    swordAttacks[swordCnt - 1].SetActive(true);
                    swordAttacks[swordCnt - 1].transform.position = bulletPos;

                    swordAttacks[swordCnt - 1].transform.rotation = transform.rotation;


                    swordAttacks[swordCnt - 1].transform.Rotate(new Vector3(0f, 0f, -90f));


                    swordCnt--;



                    if (swordCnt == 1)
                    {
                        swordCnt = 40;
                    }
                }
                else
                {
                    // 쇠뇌
                    StartCoroutine(AttackBow());

                }
                curtime = cooltime;
            }

        }



        curtime -= Time.deltaTime;

    }   // Update()


    IEnumerator AttackBow()
    {
        // 쇠뇌

        bowAni.SetTrigger("BowAttack");

        yield return new WaitForSeconds(0.5f);

        Vector3 bulletPos = bowDamagePos.transform.position;

        bowAttack[arrowCount - 1].SetActive(true);
        bowAttack[arrowCount - 1].transform.position = bulletPos;

        bowAttack[arrowCount - 1].transform.rotation = transform.rotation;

        bowAttack[arrowCount - 1].transform.Rotate(new Vector3(0f, 0f, -90f));

        arrowCount--;


        if (arrowCount == 1)
        {
            arrowCount = 0;
        }
    }

}
