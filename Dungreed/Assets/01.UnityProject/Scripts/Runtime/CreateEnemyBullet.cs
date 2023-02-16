using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CreateEnemyBullet : MonoBehaviour
{

    protected GameObject[] bullet;
    protected GameObject batFirePrefab;

    protected int bulletCnt = default;

    // 최근 생성 시점에서 지난 시간
    protected float timeAfterSpawn = default;

    // 총알 생성 주기 : 초당 몇발
    protected float spawnFireBullet = default;


    protected GameObject target = default;
    protected Transform targetPlayer = default;

    protected GameObject gameObjs = default;

    protected EnemyObjs monster = default;

    protected BoxCollider2D boxCollider = default;

}
