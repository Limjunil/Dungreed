using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Movement2D movement2D = default;
    private bool isDash = false;


    private void Awake()
    {
        movement2D = gameObject.GetComponentMust<Movement2D>();
        isDash = false;
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
            movement2D.OnJump();

        }


        if (Input.GetKey(KeyCode.Space))
        {
            movement2D.isLongJump = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            movement2D.isLongJump = false;
        }

    }

    IEnumerator StayDash()
    {
        yield return new WaitForSeconds(0.1f);
        isDash = false;
    }
}
