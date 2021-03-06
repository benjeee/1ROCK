﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ShootProjectileAtPlayer))]
public class QuadBlasterBlaster : EnemyAppendage {

    [SerializeField]
    QuadBlasterArm parentArm;

    ShootProjectileAtPlayer shooter;

    void Awake()
    {
        shooter = GetComponent<ShootProjectileAtPlayer>();
        shooter.enabled = false;
    }

    new void Start()
    {
        base.Start();
    }

    public void EnableShooting()
    {
        shooter.enabled = true;
    }

    public override void DestroyAppendage()
    {
        parentArm.DestroyBlaster();
        base.DestroyAppendage();
    }


}
