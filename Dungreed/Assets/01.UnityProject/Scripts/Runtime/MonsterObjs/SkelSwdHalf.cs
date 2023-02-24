using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkelSwdHalf : MonoBehaviour
{

    public SkelBossSword skelBossSword = default;

    // Start is called before the first frame update
    void Start()
    {

        skelBossSword = gameObject.GetComponentInParent<SkelBossSword>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            GFunc.Log("실행됨.");
            

            skelBossSword.GoSwdAttack = false;
            skelBossSword.skelBossSwdRigid.velocity = Vector3.zero;
            skelBossSword.skelSwdCollider.enabled = false;
            skelBossSword.EndSwdPattern = true;

        }
    }

}
