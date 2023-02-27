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


    // Start is called before the first frame update
    void Start()
    {
        dungeonMapPrefab = Resources.LoadAll<GameObject>("Prefabs/MapStage1/");
        dungeonMaps = new GameObject[dungeonMapPrefab.Length];

        nowDungRoom = 0;

        GameObject gameObjs_ = GFunc.GetRootObj("GameObjs");
        GameObject camera_ = gameObjs_.FindChildObj("Main Camera");
        dungeonCamera = camera_.GetComponentMust<MoveCamera>();
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
    }

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
    }

}
