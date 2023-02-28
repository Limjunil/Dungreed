using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PassMapMove : MonoBehaviour, IPointerClickHandler
{
    public DungeonManager dungeonManager = default;
    public GameObject pastDungUiObj = default;

    public int passNextdungNumber = default;

    public string passDoorName = default;


    // Start is called before the first frame update
    void Start()
    {
        SetChkPassDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (passNextdungNumber == 4)
        {
            CloseMap();
            return;
        }

        PassNextDungeon();
    }

    public void SetChkPassDungeon()
    {
        passDoorName = gameObject.name;

        switch (passDoorName)
        {
            case "PassBtn0":
                passNextdungNumber = 0;
                break;

            case "PassBtn1":
                passNextdungNumber = 1;

                break;

            case "PassBtn2":
                passNextdungNumber = 2;

                break;

            case "PassBtn3":
                passNextdungNumber = 3;

                break;

            case "MapCloseBtn":

                passNextdungNumber = 4;


                break;
        }

        GameObject pastBgObj = gameObject.transform.parent.gameObject;
        pastDungUiObj = pastBgObj.transform.parent.gameObject;

        GameObject dungeonsObjs_ = GFunc.GetRootObj("DungeonsObjs");
        dungeonManager = dungeonsObjs_.GetComponentMust<DungeonManager>();


    }   // SetChkPassDungeon()

    public void PassNextDungeon()
    {

        dungeonManager.PassNextDungeon(passNextdungNumber);
        CloseMap();
    }

    public void CloseMap()
    {

        pastDungUiObj.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
    }


}
