using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = default;

    [SerializeField]
    private float jumpForce = default;

    [SerializeField]
    private float dashSpeed = default;

    private Rigidbody2D playerRigid = default;
    private Animator animator = default;


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

    private PlayerController playerController = default;


    [SerializeField]
    // 최대 점프 횟수
    private int maxJumpCnt = default;
    // 현재 점프한 횟수
    private int currentJumpCnt = default;



    [SerializeField]
    private int maxDashCnt = default;
    private int currentDashCnt = default;

    // 대쉬 때 사용할 잔상 이미지
    public GameObject dashGhostImagePrefab;
    public GameObject[] dashGhostBgImage = default;
    public int dashImageCnt = default;

    public int ChkdashImageCnt = default;

    public GameObject dashAttack = default;
    public Image playerDashBar = default;
    public float dashAmount = default;

    public bool isPlayerDie = false;


    public GameObject gameoverTown = default;
    public Image gameoverBgImg = default;

    public string sceneName = default;

    private void Awake()
    {
        isPlayerDie = false;
        playerSpeed = 5f;
        jumpForce = 10f;
        isLongJump = false;
        dashSpeed = 15f;

        maxJumpCnt = 2;
        currentJumpCnt = 0;

        maxDashCnt = 3;
        currentDashCnt = maxDashCnt;

        playerRigid = gameObject.GetComponentMust<Rigidbody2D>();

        animator = gameObject.GetComponentMust<Animator>();

        playerCollider = gameObject.GetComponent<BoxCollider2D>();
        playerController = gameObject.GetComponentMust<PlayerController>();

        isGroundOne = false;
        isGroundTwo = false;

        groundLayer = LayerMask.GetMask("Ground");
        groundBgLayer = LayerMask.GetMask("GroundBg");

        ChkdashImageCnt = 0;

        dashImageCnt = 12;
        dashGhostBgImage = new GameObject[dashImageCnt];

        
        sceneName = SceneManager.GetActiveScene().name;

        GetDashComnent();

        GameObject gameObjs_ = GFunc.GetRootObj("GameObjs");
        GameObject etcSpawn_ = gameObjs_.FindChildObj("EtcSpawn");

        for (int i = 0; i < dashImageCnt; i++)
        {
            dashGhostBgImage[i] = Instantiate(dashGhostImagePrefab,
                gameObject.transform.position,
                Quaternion.identity, etcSpawn_.transform);

            dashGhostBgImage[i].SetActive(false);
        }

        dashAttack = gameObject.FindChildObj("DashAttack");
        dashAttack.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(ReDashCnt());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDashGauge();
    }

    private void FixedUpdate()
    {

        if(isPlayerDie == true) { return; }

        // 플레이어의 collider min, center, max 위치 정보
        Bounds bounds = playerCollider.bounds;

        // 플레이어의 발 위치 설정
        playerFootPos = new Vector2(bounds.center.x, bounds.min.y);

        // 플레이어 발 위치에 원을 생성하고, 원이 바닥과 닿으면 isGround = true
        isGroundOne = Physics2D.OverlapCircle(playerFootPos, 0.1f, groundLayer);
        isGroundTwo = Physics2D.OverlapCircle(playerFootPos, 0.1f, groundBgLayer);

        if (isGroundOne == true || isGroundTwo == true)
        {
            isGround = true;
            animator.SetBool("Ground", true);
        }
        else
        {
            isGround = false;

        }


        if (isGround == true && playerRigid.velocity.y <= 0)
        {
            currentJumpCnt = maxJumpCnt;

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
            playerController.PlayAudioDash();

            animator.SetBool("Ground", false);

            dashAttack.SetActive(true);
            Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            // 길이이자 방향이다.


            playerRigid.velocity = len.normalized * dashSpeed;

            currentDashCnt--;

            StartCoroutine(DashAlpha());

        }


    }   // OnDash()

    //! 대쉬와 게임오버 오브젝트를 가져오는 함수
    public void GetDashComnent()
    {
        GameObject uiObjs_ = GFunc.GetRootObj("UiObjs");
        GameObject playerDashBack_ = uiObjs_.FindChildObj("PlayerDashBack");
        GameObject dashBack2_ = playerDashBack_.FindChildObj("DashBack2");
        GameObject playerDashBarObj_ = dashBack2_.FindChildObj("PlayerDashBar");

        playerDashBar = playerDashBarObj_.GetComponentMust<Image>();

        gameoverTown = uiObjs_.FindChildObj("GameOverTown");


        if(sceneName == GData.SCENE_NAME_PLAY)
        {
            gameoverTown.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

            GameObject gameoverBgImgObj = gameoverTown.FindChildObj("GameoverBgImg");

            gameoverBgImg = gameoverBgImgObj.GetComponentMust<Image>();
        }
        else
        {
            /* Do Nothing */
        }
    }


    //! 실시간으로 대쉬 게이지를 알려주는 함수
    public void PlayerDashGauge()
    {
        dashAmount = currentDashCnt / (float)maxDashCnt;
        playerDashBar.fillAmount = dashAmount;
    }

    public void PlayerDieAct()
    {
        isPlayerDie = true;

        animator.SetTrigger("isPlayerDie");
        StartCoroutine(DieToTown());
    }

    public IEnumerator DieToTown()
    {
        yield return new WaitForSeconds(1f);

        gameoverTown.transform.localScale = Vector3.one;

        float countVal = 0;
        int countTime = 0;

        Color bgImg = gameoverBgImg.color;

        while (countTime < 5)
        {
            bgImg.a = countVal / 255f;

            gameoverBgImg.color = bgImg;

            countVal += 45f;

            yield return new WaitForSeconds(0.5f);

            countTime++;

        }

        bgImg.a = 255f / 255f;
        gameoverBgImg.color = bgImg;

        yield return new WaitForSeconds(0.5f);

        GFunc.LoadScene(GData.SCENE_NAME_TOWN);

    }

    public IEnumerator DashAlpha()
    {

        if(ChkdashImageCnt == 12)
        {
            ChkdashImageCnt = 0;
        }

        int countDash = ChkdashImageCnt + 4;

        for (int i = ChkdashImageCnt; i < countDash; i++)
        {

            Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float lookZ = Mathf.Atan2(len.y, len.x);

            if (-1.5f < lookZ && lookZ < 1.5f)
            {
                dashGhostBgImage[i].transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {

                dashGhostBgImage[i].transform.localScale = new Vector3(-1f, 1f, 1f);

            }

            dashGhostBgImage[i].transform.position = gameObject.transform.position;
                

            dashGhostBgImage[i].SetActive(true);

            ChkdashImageCnt++;

            yield return new WaitForSeconds(0.075f);

        }

        dashAttack.SetActive(false);

    }

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

    public void OnInDungeon()
    {
        playerRigid.velocity = Vector2.zero;

        animator.SetBool("Run", false);
        animator.SetBool("Ground", true);
    }   // OnInDungeon()



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NextDungUp" ||
           collision.tag == "NextDungDown" ||
           collision.tag == "NextDungLt" ||
           collision.tag == "NextDungRt")
        {

            for(int i = 0; i < dashGhostBgImage.Length; i++)
            {
                dashGhostBgImage[i].SetActive(false);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") ||
            collision.collider.CompareTag("GroundBg"))
        {
            animator.SetBool("Ground", true);

        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground") ||
            collision.collider.CompareTag("GroundBg"))
        {
            animator.SetBool("Ground", false);

        }
    }
}
