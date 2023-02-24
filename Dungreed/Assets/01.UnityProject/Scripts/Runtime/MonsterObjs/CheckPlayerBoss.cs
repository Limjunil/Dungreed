using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerBoss : MonoBehaviour
{
    private PlayerController playerController = default;
    private GameObject SkelBoss = default;
    private GameObject skelBossHp = default;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);

        GameObject gameObjs_ = GFunc.GetRootObj("GameObjs");
        GameObject player_ = gameObjs_.FindChildObj("Player");

        playerController = player_.GetComponentMust<PlayerController>();

        GameObject uiObjs_ = GFunc.GetRootObj("UiObjs");

        skelBossHp = uiObjs_.FindChildObj("MonsterHpBack");
        skelBossHp.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

        GameObject dungObjs = gameObject.transform.parent.gameObject;

        SkelBoss = dungObjs.FindChildObj("SkelBoss");
        SkelBoss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerController.inBoss = true;

            StartCoroutine(GoSkelBossPlay());
        }
    }

    IEnumerator GoSkelBossPlay()
    {
        yield return new WaitForSeconds(1f);

        SkelBoss.SetActive(true);
        gameObject.SetActive(false);
        skelBossHp.transform.localScale = new Vector3(3f, 3f, 3f);
        playerController.inBoss = false;
    }
}
