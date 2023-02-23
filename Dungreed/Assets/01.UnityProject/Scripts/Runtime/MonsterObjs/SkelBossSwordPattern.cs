using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBossSwordPattern : MonoBehaviour
{

    public GameObject skelBossSwdPrefabs;

    public GameObject[] skelBossSwords = default;

    public int swordCount = default;

    public Vector3 firstPosSwd = default;

    // Start is called before the first frame update
    void Start()
    {
        swordCount = 6;
        skelBossSwords = new GameObject[swordCount];
        firstPosSwd = new Vector3(-350f, 330f, 0f);

        float posX = 150f;

        for (int i = 0; i < swordCount; i++)
        {
            
            skelBossSwords[i] = Instantiate(skelBossSwdPrefabs, gameObject.transform.position, Quaternion.identity, gameObject.transform );
            skelBossSwords[i].name = skelBossSwdPrefabs.name + $"_{i + 1}";

            skelBossSwords[i].transform.localPosition = firstPosSwd;

            firstPosSwd = new Vector3(firstPosSwd.x + posX, firstPosSwd.y, 0f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
