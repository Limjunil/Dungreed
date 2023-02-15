using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public float enemySpeed = default;

    private Rigidbody2D enemyRigid;

    private int changeWay = default;

    // Start is called before the first frame update
    void Start()
    {
        enemySpeed = 1f;
        enemyRigid = gameObject.GetComponentMust<Rigidbody2D>();


        Invoke("RandomWay", 1f);
    }

    // Update is called once per frame
    void Update()
    {

        
        enemyRigid.velocity = new Vector2(changeWay, enemyRigid.velocity.y);

        if( 0 < changeWay)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);

        }
    }

    public void RandomWay()
    {
        changeWay = Random.Range(-1, 1 + 1);

        Invoke("RandomWay", 1f);

    }


}
