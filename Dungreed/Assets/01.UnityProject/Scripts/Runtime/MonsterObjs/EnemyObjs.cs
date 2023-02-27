using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyObjs
{
    protected string monsterName = string.Empty;
    protected int monsterHp;
    protected int monsterDamage;
    protected int monsterLaserDamage;

    protected int monsterExp;
    protected float monsterSpeed;
    protected bool monsterCanFly;


    public string MonsterName()
    {
        return this.monsterName;
    }

    public int MonsterHp()
    {
        return this.monsterHp;
    }

    public int MonsterDamage()
    {
        return this.monsterDamage;
    }


    public int MonsterExp()
    {
        return this.monsterExp;
    }

    public float MonsterSpeed()
    {
        return this.monsterSpeed;
    }

    public bool MonsterCanFly()
    {
        return this.monsterCanFly;
    }

    public int MonsterLaserDamage()
    {
        return this.monsterLaserDamage;
    }
}   // EnemyObjs()


class SkelNorGreatSwd : EnemyObjs
{
    public SkelNorGreatSwd()
    {
        this.monsterName = "SkelNormalGreatSword";
        this.monsterHp = 20;
        this.monsterDamage = 6;
        this.monsterExp = 20;
        this.monsterSpeed = 1f;
        this.monsterCanFly = false;
    }
}

class BatRed : EnemyObjs
{
    public BatRed()
    {
        this.monsterName = "BatRed";
        this.monsterHp = 15;
        this.monsterDamage = 4;
        this.monsterExp = 10;
        this.monsterSpeed = 1.5f;
        this.monsterCanFly = true;
    }
}

class SkelBoss : EnemyObjs
{
    public SkelBoss()
    {
        this.monsterName = "SkelBoss";
        this.monsterHp = 100;
        this.monsterDamage = 8;
        this.monsterLaserDamage = 10;
        this.monsterExp = 50;
    }
}


