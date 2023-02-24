using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBossSwordPattern : MonoBehaviour
{

    public GameObject skelBossSwdPrefabs;

    public GameObject[] skelBossSwords = default;

    public int swordCount = default;

    public Vector3 firstPosSwd = default;

    public bool GoSkelSwdPattern = false;

    // Start is called before the first frame update
    void Start()
    {
        GoSkelSwdPattern = false;
        swordCount = 6;
        skelBossSwords = new GameObject[swordCount];
        firstPosSwd = new Vector3(-350f, 130f, 0f);

        float posX = 150f;

        for (int i = 0; i < swordCount; i++)
        {
            
            skelBossSwords[i] = Instantiate(skelBossSwdPrefabs, gameObject.transform.position, Quaternion.identity, gameObject.transform );
            skelBossSwords[i].name = skelBossSwdPrefabs.name + $"_{i + 1}";

            skelBossSwords[i].transform.localPosition = firstPosSwd;

            firstPosSwd = new Vector3(firstPosSwd.x + posX, firstPosSwd.y, 0f);

            skelBossSwords[i].SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GoSkelSwdPattern == true)
        {
            StartCoroutine(StartSkelSwdPattern());
            GoSkelSwdPattern = false;
        }
    }

    public void OnSkelSwdPattern()
    {
        GoSkelSwdPattern = true;
    }

    IEnumerator StartSkelSwdPattern()
    {
        for (int i = 0; i < swordCount; i++)
        {
            skelBossSwords[i].SetActive(true);

            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(6f);

        firstPosSwd = new Vector3(-350f, 130f, 0f);
        float posX = 150f;

        for (int i = 0; i < swordCount; i++)
        {
            skelBossSwords[i].transform.localPosition = firstPosSwd;

            firstPosSwd = new Vector3(firstPosSwd.x + posX, firstPosSwd.y, 0f);

            skelBossSwords[i].SetActive(false);
        }

    }


}
