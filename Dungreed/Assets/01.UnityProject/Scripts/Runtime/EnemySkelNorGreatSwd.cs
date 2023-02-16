using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkelNorGreatSwd : EnemyMoveController
{
    public EnemyObjs enemyObjs = default;
    public Animator animator = default;

    // Start is called before the first frame update
    void Start()
    {

        enemyObjs = new SkelNorGreatSwd();

        this.enemySpeed = enemyObjs.MonsterSpeed();
        this.changeWay = default;
        this.enemyRigid = gameObject.GetComponentMust<Rigidbody2D>();
        this.target = default;
        this.targetPlayer = default;

        this.isFindPlayer = false;
        this.enemyFly = enemyObjs.MonsterCanFly();

        animator = gameObject.GetComponentMust<Animator>();

        
        Invoke("RandomWay", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
        OnMoveEnemy();
    }

    public override void RandomWay()
    {
        base.RandomWay();
    }

    public override void OnMoveEnemy()
    {
        base.OnMoveEnemy();
    }

    public override void DiscoveryPlayer()
    {
        base.DiscoveryPlayer();
    }

    public override void OnDisappearPlayer()
    {
        base.OnDisappearPlayer();
    }

    public override void OnFindPlayer()
    {
        base.OnFindPlayer();
    }
}
