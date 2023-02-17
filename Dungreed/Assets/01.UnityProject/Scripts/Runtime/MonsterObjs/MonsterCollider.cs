using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollider : MonoBehaviour
{

    [SerializeField]
    private BoxCollider2D monsterCollider = default;
    private Rigidbody2D monsterRigid = default;


    // Start is called before the first frame update
    void Start()
    {
        monsterCollider = gameObject.GetComponentMust<BoxCollider2D>();

        monsterRigid = gameObject.GetComponent<Rigidbody2D>();


        MonsterFeetOnOff(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            MonsterFeetOnOff(true);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        MonsterFeetOnOff(collision, true);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        MonsterFeetOnOff(collision, false);
    }


    public void MonsterFeetOnOff(Collider2D collision, bool isOn)
    {
        if (collision.tag == "Player")
        {

            MonsterFeetOnOff(isOn);
        }
    }


    public void MonsterFeetOnOff(bool isOn)
    {
        monsterCollider.isTrigger = isOn;
        if (isOn == true)
        {
            monsterRigid.gravityScale = 0f;

        }
        else
        {
            monsterRigid.gravityScale = 1f;
        }
    }

}