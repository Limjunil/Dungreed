using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNextDungeon : MonoBehaviour
{
    private DungeonManager dungeonManager = default;
    public string dungeonName = default;
    public string dungeonArrowDoor = default;
    public int dungeonNumber = default;

    // Start is called before the first frame update
    void Start()
    {
        GameObject dungObjs_ = gameObject.transform.parent.gameObject;

        GameObject nowDungeon_ = dungObjs_.transform.parent.gameObject;

        dungeonName = nowDungeon_.name;

        GameObject dungeonsObjs_ = nowDungeon_.transform.parent.gameObject;
        dungeonManager = dungeonsObjs_.GetComponentMust<DungeonManager>();

        dungeonArrowDoor = gameObject.tag;

        switch (dungeonName)
        {
            case "00.DungeonInGrid":
                dungeonNumber = 0;
                break;

            case "01.Dungeon1Grid":
                dungeonNumber = 1;

                break;

            case "02.Dungeon2Grid":
                dungeonNumber = 2;

                break;

            case "03.DungeonBossGateGrid":
                dungeonNumber = 3;

                break;

            case "04.DungeonBossInGrid":
                dungeonNumber = 4;

                break;

            case "05.DungeonBoss1Grid":
                dungeonNumber = 5;

                break;

            case "06.DungeonEnd":
                dungeonNumber = 6;

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextDung()
    {
        GFunc.Log($"{dungeonNumber}, {dungeonArrowDoor}");
        dungeonManager.NextDungeon(dungeonNumber, dungeonArrowDoor);
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            if (dungeonArrowDoor == "NextDungBossIn")
            {
                /* Do Nothing */
            }
            else
            {
                NextDung();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            if (dungeonArrowDoor == "NextDungBossIn")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    NextDung();
                }
            }
            else
            {
                /* Do Nothing */
            }
        }
    }
}
