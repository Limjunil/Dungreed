using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcTalking : MonoBehaviour
{
    public GameObject npcTalkUiObjs = default;

    public Text npcNameTxt = default;
    public Text npcTalkTxt = default;

    public string npcName = default;

    public bool isTalking = false;

    public int talkCount = default;

    public bool talkIn = false;

    // Start is called before the first frame update
    void Start()
    {
        talkIn = false;
        isTalking = false;
        talkCount = 0;

        switch (gameObject.name)
        {
            case "NpcBuilder":
                npcName = "율포드";
                break;

            case "NpcInn":
                npcName = "호레리카";
                break;

            case "NpcMerchant":
                npcName = "밀리아";
                break;
        }


        GameObject uiObjs_ = GFunc.GetRootObj("UiObjs");
        npcTalkUiObjs = uiObjs_.FindChildObj("NpcTalkUiObjs");

        npcNameTxt = npcTalkUiObjs.FindChildObj("NpcNameTxt").GetComponentMust<Text>();
        npcTalkTxt = npcTalkUiObjs.FindChildObj("NpcTalkTxt").GetComponentMust<Text>();

        npcTalkUiObjs.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
    }

    // Update is called once per frame
    void Update()
    {

        if (talkIn == true && Input.anyKeyDown)
        {
            npcTalkUiObjs.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            talkIn = false;
            return;
        }

        if (isTalking == true && Input.GetKeyDown(KeyCode.F))
        {
            

            talkIn = true;
            npcTalkUiObjs.transform.localScale = Vector3.one;

            NpcTalkingStart(talkCount);
        }
    }

    public void NpcTalkingStart(int countTalk)
    {
        npcNameTxt.text = npcName;

        npcTalkTxt.text = TalkPattern(countTalk);

        talkCount++;


        //if (Input.anyKeyDown)
        //{
        //    npcTalkUiObjs.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        //}
    }

    public string TalkPattern(int countTalk)
    {
        string talkTxt = default;

        if(npcName == "율포드")
        {
            switch (countTalk)
            {
                case 0:
                    talkTxt = "만나서 반갑네, 모험가!";

                    break;
                case 1:
                    talkTxt = "저 앞 촛불이 있는 곳은 위험하니 조심하게!";
                    break;
                default:
                    talkTxt = "미안하지만 바쁘니 다음에 오게나.";
                    break;
            }

        }

        if (npcName == "호레리카")
        {
            switch (countTalk)
            {
                case 0:
                    talkTxt = "저희 여관에 오신 걸 환영합니다!";

                    break;

                case 1:
                    talkTxt = "오늘은 날씨도 좋네요.";
                    break;

                default:
                    talkTxt = "저희 여관은 24시간 운영합니다!";
                    break;
            }

        }

        if (npcName == "밀리아")
        {
            switch (countTalk)
            {
                case 0:
                    talkTxt = "저희 마을은 조용하지만 좋은 곳이에요.";

                    break;

                case 1:
                    talkTxt = "저 위험한 던전만 없었더라면 좋았을 텐데...";
                    break;

                default:
                    talkTxt = "평화로운 하루네요.";
                    break;
            }

        }



        return talkTxt;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isTalking = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTalking = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTalking = false;
        }
    }

}
