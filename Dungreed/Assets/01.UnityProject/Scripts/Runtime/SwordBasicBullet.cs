using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasicBullet : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("OffSwordBullet", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OffSwordBullet()
    {
        gameObject.SetActive(false);
    }
}
