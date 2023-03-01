using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public MoveCamera dungeonCamera = default;

    public GameObject[] dungeonMapPrefab = default;

    public GameObject[] dungeonMaps = default;

    public GameObject playerObjs = default;

    public int nowDungRoom = default;

    public PlayerController playerController = default;
    public PlayerAttack playerAttack = default;
    public Movement2D movement2D = default;


    // Start is called before the first frame update
    void Start()
    {

        GameObject gameObj_ = GFunc.GetRootObj("GameObjs");

        playerController = gameObj_.FindChildObj("Player").GetComponentMust<PlayerController>();
        movement2D = gameObj_.FindChildObj("Player").GetComponentMust<Movement2D>();
        playerAttack = gameObj_.FindChildObj("Player").FindChildObj("RotateWea").GetComponentMust<PlayerAttack>();

        dungeonMapPrefab = Resources.LoadAll<GameObject>("Prefabs/MapStage1/");
        dungeonMaps = new GameObject[dungeonMapPrefab.Length];

        nowDungRoom = 0;

        GameObject gameObjs_ = GFunc.GetRootObj("GameObjs");
        dungeonCamera = Camera.main.gameObject.GetComponentMust<MoveCamera>();
        playerObjs = gameObjs_.FindChildObj("Player");

        Vector3 mapPos = Vector3.zero;
        for (int i = 0; i < dungeonMapPrefab.Length; i++)
        {
            dungeonMaps[i] = Instantiate(dungeonMapPrefab[i], mapPos, Quaternion.identity, gameObject.transform);

            dungeonMaps[i].name = dungeonMapPrefab[i].name;

            dungeonMaps[i].SetActive(false);
        }

        OnDungeon(nowDungRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //! 던전 활성화 함수
    public void OnDungeon(int dungNumer)
    {
        dungeonCamera.SetCameraRange(dungNumer);
        playerObjs.transform.localPosition = Vector3.zero;
        dungeonMaps[dungNumer].SetActive(true);
    }   // OnDungeon()

    public void NextDungeon(int dungNumber, string arrowDoor)
    {
        dungeonMaps[dungNumber].SetActive(false);

        if(dungNumber == 0)
        {
            nowDungRoom = 2;

            OnDungeon(nowDungRoom);

            playerObjs.transform.localPosition = new Vector3(720f, -380f, 0f);

        }

        if(dungNumber == 1)
        {
            nowDungRoom = 2;

            OnDungeon(nowDungRoom);

            playerObjs.transform.localPosition = new Vector3(-60f, -20f, 0f);
        }

        if(dungNumber == 2)
        {
            switch (arrowDoor)
            {                
                case "NextDungDown":
                    nowDungRoom = 0;

                    OnDungeon(nowDungRoom);

                    playerObjs.transform.localPosition = new Vector3(580f, 440f, 0f);

                    break;

                case "NextDungLt":
                    nowDungRoom = 1;

                    OnDungeon(nowDungRoom);

                    playerObjs.transform.localPosition = new Vector3(810f, -25f, 0f);

                    break;

                case "NextDungRt":
                    nowDungRoom = 3;

                    OnDungeon(nowDungRoom);
                    break;
            }
        }

        if(dungNumber == 3)
        {
            switch (arrowDoor)
            {
                case "NextDungLt":
                    nowDungRoom = 2;

                    OnDungeon(nowDungRoom);

                    playerObjs.transform.localPosition = new Vector3(1486f, -25f, 0f);

                    break;

                case "NextDungBossIn":
                    nowDungRoom = 4;

                    OnDungeon(nowDungRoom);

                    break;
            }
        }

        if (dungNumber == 4)
        {
            switch (arrowDoor)
            {
                case "NextDungRt":
                    nowDungRoom = 5;

                    OnDungeon(nowDungRoom);

                    break;
            }
        }

        if(dungNumber == 5)
        {
            switch (arrowDoor)
            {

                case "NextDungRt":
                    nowDungRoom = 6;

                    OnDungeon(nowDungRoom);

                    break;
            }
        }
        movement2D.AllOffDashAlpha();
        playerAttack.AllOffBulletObjs();

    }   // NextDungeon()


    public void PassNextDungeon(int passDungNumber)
    {
        for(int i = 0; i < dungeonMaps.Length; i++)
        {
            dungeonMaps[i].SetActive(false);

        }

        switch (passDungNumber)
        {
            case 0:
                nowDungRoom = 0;

                OnDungeon(nowDungRoom);

                playerObjs.transform.localPosition = new Vector3(264f, 0f, 0f);

                break;

            case 1:
                nowDungRoom = 1;

                OnDungeon(nowDungRoom);

                playerObjs.transform.localPosition = new Vector3(-20f, -20f, 0f);

                break;

            case 2:
                nowDungRoom = 2;

                OnDungeon(nowDungRoom);

                playerObjs.transform.localPosition = new Vector3(928f, -388f, 0f);

                break;

            case 3:
                nowDungRoom = 3;

                OnDungeon(nowDungRoom);

                playerObjs.transform.localPosition = new Vector3(290f, -28f, 0f);

                break;
        }

        movement2D.AllOffDashAlpha();

        playerAttack.AllOffBulletObjs();
        playerController.OffPlzIgnore();
    }   // PassNextDungeon()


}
