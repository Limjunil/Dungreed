using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Movement2D movement2D = default;
    private bool isDash = false;

    public int playerLayer = default;
    public int groundBgLayer = default;


    private Rigidbody2D playerRigid = default;

    public bool isIgnore = true;

    public bool isDungeon = false;

    private void Awake()
    {
        isDungeon = false;
        isIgnore = true;
        movement2D = gameObject.GetComponentMust<Movement2D>();
        isDash = false;

        playerLayer = LayerMask.NameToLayer("Player");
        groundBgLayer = LayerMask.NameToLayer("GroundBg");

        playerRigid = gameObject.GetComponentMust<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
                

    }

    // Update is called once per frame
    void Update()
    {
              

        float x = Input.GetAxisRaw("Horizontal");

        if (isDungeon == true)
        {
            movement2D.OnInDungeon();
            return;
        }

        if (isDash == false)
        {
            movement2D.OnMove(x);
        }

        if (Input.GetMouseButtonDown(1) && !isDash)
        {
            isIgnore = true;
            isDash = true;
            movement2D.OnDash();
            Invoke("OnIsIgnore", 0.4f);
            StartCoroutine(StayDash());
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            isIgnore = true;

            if (Input.GetKey(KeyCode.S))
            {
                playerRigid.velocity = Vector2.down;
            }
            else
            {
                movement2D.OnJump();

            }

            Invoke("OnIsIgnore", 0.4f);

        }

        
        if (Input.GetKey(KeyCode.Space))
        {

            movement2D.isLongJump = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            movement2D.isLongJump = false;

        }


        if (isIgnore == true)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, groundBgLayer, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(playerLayer, groundBgLayer, false);

        }

    }

    IEnumerator StayDash()
    {
        
        yield return new WaitForSeconds(0.6f);
        isDash = false;

        GFunc.Log("tlfgod");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            GFunc.Log("땅에 닿았다");
            isIgnore = true;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dungeon")
        {
            GFunc.Log("던전에 진입합니다.");

            isDungeon = true;
            playerRigid.velocity = Vector2.zero;

            StartCoroutine(StartDungeon());

        }

        if (collision.tag == "FirstNoDown")
        {
            GFunc.Log("첫 진입 중!");
            isIgnore = false;

        }

        if(collision.tag == "SkelSword")
        {
            GFunc.Log("해골에게 공격 받았다!");
        }

        if(collision.tag == "BatFire")
        {
            GFunc.Log("박쥐에게 공격 받았다!");
        }

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "NoZone")
        {
            isIgnore = true;

        }

        if (collision.tag == "GroundBg")
        {
            if(playerRigid.velocity.y == 0)
            {
                isIgnore = true;
            }
            else
            {
                isIgnore = false;
                GFunc.Log("트리거에서 호출");
            }
        }

        if (collision.tag == "FirstNoDown")
        {
            GFunc.Log("첫 진입 중!");
            isIgnore = false;

        }


    }



    IEnumerator StartDungeon()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }

    public void OnIsIgnore()
    {
        isIgnore = false;
    }
}
