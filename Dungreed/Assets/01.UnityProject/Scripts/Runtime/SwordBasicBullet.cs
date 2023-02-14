using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasicBullet : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("DestroyBullet", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
