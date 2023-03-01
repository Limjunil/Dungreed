using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    private Rigidbody2D arrowRigid = default;

    public float arrowSpeed = default;

    // Start is called before the first frame update
    void Start()
    {
        arrowRigid = gameObject.GetComponentMust<Rigidbody2D>();

        arrowSpeed = 8f;

        arrowRigid.velocity = transform.up * arrowSpeed;
    }
    void OnEnable()
    {
        StartCoroutine("StartAutoOffArrow");

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            StopCoroutine("StartAutoOffArrow");
            StartCoroutine(OffArrow());
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            StopCoroutine("StartAutoOffArrow");
            StartCoroutine(OffArrow());
        }
    }

    IEnumerator StartAutoOffArrow()
    {
        yield return new WaitForSeconds(4f);

        StartCoroutine(OffArrow());

    }

    IEnumerator OffArrow()
    {
        arrowRigid.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }
}
