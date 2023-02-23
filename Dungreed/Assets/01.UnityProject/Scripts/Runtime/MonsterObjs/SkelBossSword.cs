using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkelBossSword : MonoBehaviour
{

    private Rigidbody2D skelBossSwdRigid = default;

    public float swordSpeed = default;

    public GameObject target = default;
    public Transform targetPlayer = default;

    // Start is called before the first frame update
    void Start()
    {
        swordSpeed = 10f;

        skelBossSwdRigid = gameObject.GetComponentMust<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        target = GameObject.FindWithTag("Player");
        targetPlayer = target.transform;


        Vector2 lenSwd = targetPlayer.position - gameObject.transform.position;

        float lookZ = Mathf.Atan2(lenSwd.y, lenSwd.x);

        float z = lookZ * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, z);

        if (-1.5f < lookZ && lookZ < 1.5f)
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {

            gameObject.transform.localScale = new Vector3(1f, -1f, 1f);

        }

        //skelBossSwdRigid.velocity = transform.up * swordSpeed;
    }
}
