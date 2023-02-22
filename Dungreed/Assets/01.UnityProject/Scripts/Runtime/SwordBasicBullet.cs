using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasicBullet : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        StartCoroutine(OffGameObjs());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OffGameObjs()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

}
