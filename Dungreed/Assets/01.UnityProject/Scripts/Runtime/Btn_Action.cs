using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Action : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //! 게임을 시작하는 함수
    public void OnPlayClick()
    {
        // 마을 씬 불러오기
        GFunc.LoadScene(GData.SCENE_NAME_TOWN);
    }   // OnPlayClick()

    //! 게임을 나가는 함수
    public void OnExitClick()
    {
        GFunc.QuitThisGame();
    }
}
