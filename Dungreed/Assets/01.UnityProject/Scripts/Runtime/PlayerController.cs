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

    public bool isGround = true;

    public bool isDungeon = false;

    private void Awake()
    {
        isDungeon = false;
        isGround = true;
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
            isDash = true;
            isGround = true;
            movement2D.OnDash();
            StartCoroutine(StayDash());
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.S))
            {
                isGround = true;
                playerRigid.velocity = Vector2.down;
            }
            else
            {
                isGround = false;
                movement2D.OnJump();

            }

        }

        
        if (Input.GetKey(KeyCode.Space))
        {

            movement2D.isLongJump = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            movement2D.isLongJump = false;

        }


        if (isGround == true || 
            movement2D.isLongJump == true)
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
        isGround = false;
        isDash = false;

        GFunc.Log("tlfgod");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
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
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "NoZone")
        {
            isGround = true;
        }
    }

    IEnumerator StartDungeon()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
}
