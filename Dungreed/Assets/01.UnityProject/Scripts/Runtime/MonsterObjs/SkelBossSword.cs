using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkelBossSword : MonoBehaviour
{

    public Rigidbody2D skelBossSwdRigid = default;

    public float swordSpeed = default;

    public GameObject target = default;
    public Transform targetPlayer = default;
    public BoxCollider2D skelSwdCollider = default;
    public Canvas skelSwdSort = default;

    public bool GoSwdAttack = false;
    public bool EndSwdPattern = false;


    // Start is called before the first frame update
    void Start()
    {
        EndSwdPattern = false;
        GoSwdAttack = false;
        swordSpeed = 10f;

        skelBossSwdRigid = gameObject.GetComponentMust<Rigidbody2D>();
        skelSwdCollider = gameObject.GetComponentMust<BoxCollider2D>();
        skelSwdSort = gameObject.GetComponentMust<Canvas>();

        skelSwdSort.sortingLayerName = "Ground";
        skelSwdSort.sortingOrder = 1;

        skelSwdCollider.enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(StartAttackSwd());
    }

    private void OnDisable()
    {
        ResetSwd();
    }


    // Update is called once per frame
    void Update()
    {
        if(GoSwdAttack == false)
        {
            if(EndSwdPattern == true) { return; }
            TargetPlayerSwd();

        }
        else
        {
            skelBossSwdRigid.velocity = transform.up * -1 * swordSpeed;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            skelSwdSort.sortingLayerName = "Default";
            skelSwdSort.sortingOrder = 0;

        }
    }

    public void TargetPlayerSwd()
    {
        target = GameObject.FindWithTag("Player");
        targetPlayer = target.transform;


        Vector2 directionSwd = new Vector2(
            gameObject.transform.position.x - targetPlayer.position.x,
            gameObject.transform.position.y - targetPlayer.position.y);


        float angleSwd = Mathf.Atan2(directionSwd.y, directionSwd.x) *
            Mathf.Rad2Deg;

        Quaternion angleSwdAxis = Quaternion.AngleAxis(angleSwd - 90f, Vector3.forward);
        Quaternion rotationSwd = Quaternion.Slerp(transform.rotation, angleSwdAxis,
            swordSpeed * Time.deltaTime);

        transform.rotation = rotationSwd;
    }   // TargetPlayerSwd()

    IEnumerator StartAttackSwd()
    {
        yield return new WaitForSeconds(3f);
        skelSwdSort.sortingLayerName = "Ground";
        skelSwdSort.sortingOrder = 1;
        skelSwdCollider.enabled = true;
        GoSwdAttack = true;
    }


    public void ResetSwd()
    {
        
        GoSwdAttack = false;
        EndSwdPattern = false;
    }

}
