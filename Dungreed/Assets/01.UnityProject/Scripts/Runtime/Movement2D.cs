using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = default;

    [SerializeField]
    private float jumpForce = default;


    private Rigidbody2D playerRigid = default;
    private Animator animator = default;

    [HideInInspector]
    public bool isLongJump = false;


    [SerializeField]
    private LayerMask groundLayer;  // 바닥 체크를 위한 충돌 레이어
    [SerializeField]

    private LayerMask groundBgLayer;  // 바닥 체크를 위한 충돌 레이어

    private BoxCollider2D playerCollider; // 플레이어의 충돌 범위
    private bool isGround = false;  // 바닥 체크 / 바닥에 닿으면 true
    private bool isGroundOne = false;
    private bool isGroundTwo = false;
    private Vector3 playerFootPos;  // 플레이어 발 위치


    [SerializeField]
    // 최대 점프 횟수
    private int maxJumpCnt = default;
    // 현재 점프한 횟수
    private int currentJumpCnt = default;


    [SerializeField]
    private int maxDashCnt = default;
    private int currentDashCnt = default;


    private void Awake()
    {
        playerSpeed = 5f;
        jumpForce = 8f;
        isLongJump = false;

        maxJumpCnt = 2;
        currentJumpCnt = 0;

        maxDashCnt = 3;
        currentDashCnt = maxDashCnt;

        playerRigid = gameObject.GetComponentMust<Rigidbody2D>();

        animator = gameObject.GetComponentMust<Animator>();

        playerCollider = gameObject.GetComponent<BoxCollider2D>();

        isGroundOne = false;
        isGroundTwo = false;


}

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReDashCnt());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // 플레이어의 collider min, center, max 위치 정보
        Bounds bounds = playerCollider.bounds;

        // 플레이어의 발 위치 설정
        playerFootPos = new Vector2(bounds.center.x, bounds.min.y);

        // 플레이어 발 위치에 원을 생성하고, 원이 바닥과 닿으면 isGround = true
        isGroundOne = Physics2D.OverlapCircle(playerFootPos, 0.1f, groundLayer);
        isGroundTwo = Physics2D.OverlapCircle(playerFootPos, 0.1f, groundBgLayer);

        if(isGroundOne == true || isGroundTwo == true)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }


        if (isGround == true && playerRigid.velocity.y <= 0)
        {
            currentJumpCnt = maxJumpCnt;
            animator.SetBool("Ground", true);

        }


        if (isLongJump == true && 0 < playerRigid.velocity.y)
        {
            playerRigid.gravityScale = 1.0f;
        }

        else
        {
            playerRigid.gravityScale = 2.5f;
        }
    }


    public void OnMove(float x)
    {

        
        playerRigid.velocity = new Vector2(x * playerSpeed, playerRigid.velocity.y);
       

        if(x == 0)
        {
            animator.SetBool("Run", false);

        }
        else
        {
            animator.SetBool("Run", true);

        }

    }

    public void OnJump()
    {
        if(0 < currentJumpCnt)
        {
            playerRigid.velocity = Vector2.up * jumpForce;

            animator.SetBool("Ground", false);

            currentJumpCnt--;
        }


    }

    public void OnDash()
    {
        if (0 < currentDashCnt)
        {
            Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            // 길이이자 방향이다.


            playerRigid.velocity = len.normalized * 15f;

            currentDashCnt--;

        }


    }   // OnDash()

    public IEnumerator ReDashCnt()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (currentDashCnt == maxDashCnt)
            {
                /* Do Nothing */
            }
            else
            {
                currentDashCnt++;

            }
        }

    }   // ReDashCnt()

}
