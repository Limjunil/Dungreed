using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteleControl : MonoBehaviour
{
    private Animator steleAni = default;

    public GameObject monsterObjs = default;
    public int ChkMosterVal = default;



    // Start is called before the first frame update
    void Start()
    {
        steleAni = gameObject.GetComponentMust<Animator>();

        GameObject dungObjs_ = gameObject.transform.parent.gameObject;
        monsterObjs = dungObjs_.FindChildObj("MonsterObjs");
        ChkMosterVal = monsterObjs.transform.childCount;


        StartCoroutine(StartSteleAnime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MinusCount()
    {
        GFunc.Log($"{ChkMosterVal}");

        ChkMosterVal--;

        if(ChkMosterVal == 0)
        {
            AllOffStele();
            EndStele();

        }
    }
    IEnumerator StartSteleAnime()
    {
        yield return new WaitForSeconds(1f);
        steleAni.SetBool("ChkMonster", true);
        GFunc.Log("참됨");
    }

    IEnumerator EndSteleAnime()
    {
        yield return new WaitForSeconds(1f);

        steleAni.SetBool("ChkMonster", false);

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

    }

    public void EndStele()
    {
        StartCoroutine(EndSteleAnime());

    }

    public void AllOffStele()
    {
        GameObject dungObjs_ = monsterObjs.transform.parent.gameObject;
        GameObject dungPrefab_ = dungObjs_.transform.parent.gameObject;

        if (dungPrefab_.name == "02.Dungeon2Grid" &&
            gameObject.name == "Stele")
        {
            GameObject stele1_ = dungObjs_.FindChildObj("Stele1");
            GameObject stele2_ = dungObjs_.FindChildObj("Stele2");

            SteleControl stele1Control = stele1_.GetComponentMust<SteleControl>();
            SteleControl stele2Control = stele2_.GetComponentMust<SteleControl>();

            stele1Control.EndStele();
            stele2Control.EndStele();

        }
    }

}