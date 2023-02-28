using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D[] cursorImg = default;
    public Texture2D cursorImgNow = default;
    public string nowScene = default;
    public int cursorVal = default;

    // Start is called before the first frame update
    void Start()
    {

        cursorVal = 2;
        cursorImg = new Texture2D[cursorVal];

        cursorImg[0] = Resources.Load<Texture2D>("Cursor/BasicCursor");
        cursorImg[1] = Resources.Load<Texture2D>("Cursor/ShootingCursor2");

        nowScene = SceneManager.GetActiveScene().name;


        if(nowScene == GData.SCENE_NAME_TITLE)
        {
            cursorImgNow = cursorImg[0];
        }
        else
        {
            cursorImgNow = cursorImg[1];
        }

        Cursor.SetCursor(cursorImgNow, Vector2.zero, CursorMode.ForceSoftware);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
