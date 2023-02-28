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
    public float cursorHalfWidth = default;
    public float cursorHalfHeight = default;


    // Start is called before the first frame update
    void Start()
    {

        cursorVal = 2;
        cursorImg = new Texture2D[cursorVal];

        cursorImg[0] = Resources.Load<Texture2D>("Sprites/Cursor/BasicCursor");
        cursorImg[1] = Resources.Load<Texture2D>("Sprites/Cursor/ShootingCursor2");


        nowScene = SceneManager.GetActiveScene().name;


        if (nowScene == GData.SCENE_NAME_TITLE)
        {
            cursorImgNow = cursorImg[0];
            cursorHalfWidth = cursorImg[0].width / 2f;
            cursorHalfHeight = cursorImg[0].height / 2f;
        }
        else
        {
            cursorImgNow = cursorImg[1];
            cursorHalfWidth = cursorImg[1].width / 2f;
            cursorHalfHeight = cursorImg[1].height / 2f;
        }

        Cursor.SetCursor(cursorImgNow, new Vector2(cursorHalfWidth, cursorHalfHeight), CursorMode.ForceSoftware);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
