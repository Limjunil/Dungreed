using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Movement2D movement2D = default;


    private void Awake()
    {
        movement2D = gameObject.GetComponentMust<Movement2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
                

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        movement2D.OnMove(x);

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


}
