using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatRed : EnemyMoveController
{
    public EnemyObjs enemyObjs = default;


    // Start is called before the first frame update
    void Start()
    {
        
        enemyObjs = new BatRed();

        this.enemySpeed = enemyObjs.MonsterSpeed();
        this.changeWay = default;
        this.enemyRigid = gameObject.GetComponentMust<Rigidbody2D>();
        this.target = default;
        this.targetPlayer = default;

        this.isFindPlayer = false;
        this.enemyFly = enemyObjs.MonsterCanFly();

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
