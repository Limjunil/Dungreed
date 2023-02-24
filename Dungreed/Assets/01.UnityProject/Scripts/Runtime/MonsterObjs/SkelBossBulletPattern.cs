using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkelBossBulletPattern : MonoBehaviour
{

    private GameObject[] skelBossUp = default;
    private GameObject[] skelBossDown = default;

    private GameObject[] skelBossLt = default;
    private GameObject[] skelBossRt = default;
    private bool startBulletPattern = false;

    public float rotateSpeed = default;
    public GameObject skelBossBulletPrefabs;
    public int bulletCnt = default;
    public bool firstStart = false;
    public Vector2 firstBulletPos = default;
    public bool OnResetPos = false;

    public Animator skelBossBulletAni = default;
    public SkelBossController skelBossController = default;

    // Start is called before the first frame update
    void Start()
    {
        OnResetPos = false;
        startBulletPattern = false;
        firstStart = false;
        bulletCnt = 20;
        rotateSpeed = 30f;
        firstBulletPos = gameObject.transform.position;

        GameObject skelBossBack_ = gameObject.transform.parent.gameObject;

        skelBossBulletAni = skelBossBack_.GetComponentInParent<Animator>();
        skelBossController = gameObject.GetComponentInParent<SkelBossController>();

        skelBossUp = new GameObject[bulletCnt];
        skelBossDown = new GameObject[bulletCnt];
        skelBossLt = new GameObject[bulletCnt];
        skelBossRt = new GameObject[bulletCnt];


        for (int i = 0; i < bulletCnt; i++)
        {
            skelBossUp[i] = Instantiate(skelBossBulletPrefabs, firstBulletPos,
                Quaternion.identity, gameObject.transform);
            skelBossDown[i] = Instantiate(skelBossBulletPrefabs, firstBulletPos,
                Quaternion.Euler(0f, 0f, 180f), gameObject.transform);

            skelBossLt[i] = Instantiate(skelBossBulletPrefabs, firstBulletPos,
                Quaternion.Euler(0f, 0f, 90f), gameObject.transform);
            skelBossRt[i] = Instantiate(skelBossBulletPrefabs, firstBulletPos,
                Quaternion.Euler(0f, 0f, -90f), gameObject.transform);

            skelBossUp[i].SetActive(false);
            skelBossDown[i].SetActive(false);
            skelBossLt[i].SetActive(false);
            skelBossRt[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(skelBossController.isDieSkelBoss == true) { return; }

        if(startBulletPattern == true)
        {
            //gameObject.transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);

            if(firstStart == false)
            {
                skelBossBulletAni.SetTrigger("SkelBossBullet");
                int randomVal = Random.Range(-1, 1 + 1);
                StartCoroutine(OnSkelBossBullets(randomVal));
                firstStart = true;
            }
        }

        if(OnResetPos == true)
        {
            skelBossBulletAni.SetTrigger("EndBullet");

            StartCoroutine(ReSetPos());
            OnResetPos = false;
        }
    }


    IEnumerator OnSkelBossBullets(int randomVal)
    {

        if(randomVal == 0)
        {
            randomVal = 1;
        }

        float rotateVal = 0f;

        for(int i = 0; i < bulletCnt; i++)
        {

            skelBossUp[i].transform.Rotate(new Vector3(0f, 0f, rotateVal));
            skelBossDown[i].transform.Rotate(new Vector3(0f, 0f, rotateVal));
            skelBossLt[i].transform.Rotate(new Vector3(0f, 0f, rotateVal));
            skelBossRt[i].transform.Rotate(new Vector3(0f, 0f, rotateVal));

            skelBossUp[i].SetActive(true);
            skelBossDown[i].SetActive(true);
            skelBossLt[i].SetActive(true);
            skelBossRt[i].SetActive(true);

            rotateVal += -15f * randomVal;

            yield return new WaitForSeconds(0.3f);
        }

        firstStart = false;
        startBulletPattern = false;
        OnResetPos = true;
    }

    public void OnStartBulletPattern()
    {
        startBulletPattern = true;
    }

    IEnumerator ReSetPos()
    {

        yield return new WaitForSeconds(3.5f);

        //gameObject.transform.rotation = Quaternion.identity;

        for (int i = 0; i < bulletCnt; i++)
        {
            skelBossUp[i].transform.rotation = Quaternion.identity;
            skelBossDown[i].transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            skelBossLt[i].transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            skelBossRt[i].transform.rotation = Quaternion.Euler(0f, 0f, -90f);

            skelBossUp[i].transform.position = firstBulletPos;
            skelBossDown[i].transform.position = firstBulletPos;
            skelBossLt[i].transform.position = firstBulletPos;
            skelBossRt[i].transform.position = firstBulletPos;

            skelBossUp[i].SetActive(false);
            skelBossDown[i].SetActive(false);
            skelBossLt[i].SetActive(false);
            skelBossRt[i].SetActive(false);
        }

        GFunc.Log("end");
    }
}
