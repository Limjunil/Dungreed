using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InDungeonDoor : MonoBehaviour
{

    public GameObject dungeonDoor;
    private GameObject target = default;
    private Transform targetPlayer = default;



    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObjs_ = GFunc.GetRootObj("GameObjs");
        GameObject dungeonPos = gameObjs_.FindChildObj("DungeonPos");

        dungeonDoor = Instantiate(dungeonDoor, gameObject.transform.position, Quaternion.identity,
            dungeonPos.transform);
        dungeonDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {

    //        GFunc.Log("던전 입구 실행됨");

    //        target = GameObject.FindWithTag("Player");
    //        targetPlayer = target.transform;

    //        dungeonDoor.transform.position = targetPlayer.transform.position;
    //        dungeonDoor.SetActive(true);
    //    }
    //}

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
        }
    }
}
