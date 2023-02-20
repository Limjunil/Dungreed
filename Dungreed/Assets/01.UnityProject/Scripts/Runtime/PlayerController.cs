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

    private void Awake()
    {
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

        if (isDash == false)
        {
            movement2D.OnMove(x);
        }

        if (Input.GetMouseButtonDown(1) && !isDash)
        {
            isDash = true;
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
        yield return new WaitForSeconds(0.1f);
        isDash = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("GroundBg"))
        {

        }
    }
}
