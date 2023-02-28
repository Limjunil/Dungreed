using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAlphaImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(OffDashAlpha());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OffDashAlpha()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
