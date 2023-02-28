using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TitleObjScroll : MonoBehaviour
{
    public Vector3 targetPos = default;

    public float scrollRange = default;

    public float moveSpeed = default;

    public Vector3 moveDirection = default;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "BackCloud1" ||
           gameObject.name == "BackCloud2")
        {
            GameObject cloudCanves_ = GFunc.GetRootObj("CloudCanves");
            targetPos = cloudCanves_.FindChildObj("BackCloud2").transform.localPosition;

            scrollRange = -1716f;
            moveSpeed = 5f;
        }

        if(gameObject.name == "FrontCloud1" || 
            gameObject.name == "FrontCloud2")
        {
            GameObject cloudCanves_ = GFunc.GetRootObj("CloudCanves");
            targetPos = cloudCanves_.FindChildObj("FrontCloud2").transform.localPosition;

            scrollRange = -1590f;
            moveSpeed = 4f;
        }

        if (gameObject.name == "MidCloud1" ||
            gameObject.name == "MidCloud2")
        {
            GameObject cloudCanves_ = GFunc.GetRootObj("CloudCanves");
            targetPos = cloudCanves_.FindChildObj("MidCloud2").transform.localPosition;

            scrollRange = -480f;
            moveSpeed = 6f;
        }



        moveDirection = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (transform.localPosition.x <= scrollRange)
        {
            transform.localPosition = targetPos;
        }
    }
}
