using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollider : MonoBehaviour
{

    [SerializeField]
    private BoxCollider2D monsterCollider = default;
    private Rigidbody2D monsterRigid = default;

    public bool isSkelDie = false;

    // Start is called before the first frame update
    void Start()
    {
        isSkelDie = false;
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
            if (isSkelDie == true) { return; }
            MonsterFeetOnOff(true);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (isSkelDie == true) { return; }

        MonsterFeetOnOff(collision, true);

        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (isSkelDie == true) { return; }

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


    public void MonsterFeetOnOff(bool isOn, float gravity)
    {
        monsterCollider.isTrigger = isOn;
        if (isOn == true)
        {
            monsterRigid.gravityScale = gravity;

        }
        else
        {
            monsterRigid.gravityScale = gravity;
        }
    }
    public void SkelDie(bool isOn, float gravity)
    {
        MonsterFeetOnOff(isOn, gravity);

        isSkelDie = true;
    }

}
