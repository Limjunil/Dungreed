using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class InDungeonDoor : MonoBehaviour
{

    private GameObject target = default;
    private Transform targetPlayer = default;

    public GameObject dungeonDoor = default;
    public GameObject inDungImage = default;
    public Image DungBgImage = default;



    // Start is called before the first frame update
    void Start()
    {
        dungeonDoor = Resources.Load<GameObject>("Prefabs/DungeonDoor");

        GameObject gameObjs_ = GFunc.GetRootObj("GameObjs");
        GameObject dungeonPos = gameObjs_.FindChildObj("DungeonPos");

        dungeonDoor = Instantiate(dungeonDoor, gameObject.transform.position, Quaternion.identity,
            dungeonPos.transform);
        dungeonDoor.SetActive(false);

        GameObject uiObjs_ = GFunc.GetRootObj("UiObjs");

        inDungImage = uiObjs_.FindChildObj("InDungImage");
        inDungImage.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        DungBgImage = inDungImage.FindChildObj("DungBgImg").GetComponentMust<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            GFunc.Log("던전 입구 실행됨");

            target = GameObject.FindWithTag("Player");
            targetPlayer = target.transform;

            float changeY = targetPlayer.transform.position.y + -0.6f;

            dungeonDoor.transform.position = new Vector2(targetPlayer.transform.position.x, changeY);

            dungeonDoor.SetActive(true);

            StartCoroutine(InDungeon());
        }
    }


    IEnumerator InDungeon()
    {
        yield return new WaitForSeconds(0.8f);

        inDungImage.transform.localScale = Vector3.one;

        Color bgImg = DungBgImage.color;

        float countVal = 0;
        int countTime = 0;

        while (countTime < 5)
        {
            bgImg.a = countVal / 255f;

            DungBgImage.color = bgImg;

            countVal += 45f;

            yield return new WaitForSeconds(0.5f);

            countTime++;

        }

        bgImg.a = 255f / 255f;
        DungBgImage.color = bgImg;

        yield return new WaitForSeconds(0.5f);

        GFunc.LoadScene(GData.SCENE_NAME_PLAY);
    }
}
