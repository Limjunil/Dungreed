using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    private Movement2D movement2D = default;
    private bool isDash = false;
    private Rigidbody2D playerRigid = default;

    public int playerLayer = default;
    public int groundBgLayer = default;

    public Image playerHpBar = default;
    public GameObject playerHpTxt = default;
    public GameObject playerLevelTxt = default;

    public bool isIgnore = true;

    public bool isDungeon = false;

    public bool inBoss = false;

    public int playerHpMax = default;
    public int playercurrentHp = default;
    public float playerAmount = default;

    public int playerLevel = default;

    private void Awake()
    {
        inBoss = false;
        isDungeon = false;
        isIgnore = false;
        movement2D = gameObject.GetComponentMust<Movement2D>();
        isDash = false;

        playerLayer = LayerMask.NameToLayer("Player");
        groundBgLayer = LayerMask.NameToLayer("GroundBg");

        playerRigid = gameObject.GetComponentMust<Rigidbody2D>();


        playerHpMax = 80;
        playercurrentHp = playerHpMax;
        playerLevel = 1;

        GetPlayerHpComnent();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerHpandLevelVal();


        //GFunc.Log($"{gameObject.transform.position} {gameObject.transform.localPosition}");
        if (inBoss == true) 
        {
            playerRigid.velocity = Vector3.zero;
            return; 
        }     

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
            StartCoroutine(OnIsIgnore());
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

            StartCoroutine(OnIsIgnore());

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

    

    //! 플레이어의 Hp 오브젝트를 가져오는 함수
    public void GetPlayerHpComnent()
    {
        GameObject uiObjs_ = GFunc.GetRootObj("UiObjs");
        GameObject playerHpBack = uiObjs_.FindChildObj("PlayerHpBack");
        GameObject hpBack2 = playerHpBack.FindChildObj("HpBack2");

        GameObject playerHpBarObj = hpBack2.FindChildObj("PlayerHpBar");
        playerHpTxt = playerHpBarObj.FindChildObj("PlayerHpTxt");

        playerHpBar = playerHpBarObj.GetComponentMust<Image>();

        GameObject playerLevelObj = hpBack2.FindChildObj("PlayerLevelUi");
        playerLevelTxt = playerLevelObj.FindChildObj("PlayerLevelTxt");


    }

    //! 실시간으로 플레이어의 HP를 보여주는 함수
    public void PlayerHpandLevelVal()
    {
        playerAmount = playercurrentHp / (float)playerHpMax;
        playerHpBar.fillAmount = playerAmount;

        playerHpTxt.SetTmpText($"{playercurrentHp} / {playerHpMax}");
        playerLevelTxt.SetTmpText($"{playerLevel}");

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

    IEnumerator StayDash()
    {

        yield return new WaitForSeconds(0.5f);
        isDash = false;

        GFunc.Log("tlfgod");
    }

    IEnumerator StartDungeon()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }

    IEnumerator OnIsIgnore()
    {
        yield return new WaitForSeconds(0.4f);
        isIgnore = false;
    }
}
