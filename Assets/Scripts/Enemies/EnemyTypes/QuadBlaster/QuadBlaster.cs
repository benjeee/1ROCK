using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ChaseInAir))]
public class QuadBlaster : Enemy {

    [SerializeField]
    QuadBlasterBlaster qbUL;
    [SerializeField]
    QuadBlasterBlaster qbUR;
    [SerializeField]
    QuadBlasterBlaster qbBL;
    [SerializeField]
    QuadBlasterBlaster qbBR;

    ChaseInAir chaseInAir;

    int numBlasters;

    void Start()
    {
        qbUL.EnableShooting();
        qbUR.Invoke("EnableShooting", .25f);
        qbBR.Invoke("EnableShooting", .5f);
        qbBL.Invoke("EnableShooting", .75f);

        chaseInAir = GetComponent<ChaseInAir>();

        numBlasters = 4;
    }


    public override void NotifyAppendageDestroyed(EnemyAppendage appendage)
    {
        if(appendage is QuadBlasterBlaster)
        {
            numBlasters--;
        }
        if(numBlasters == 0)
        {
            chaseInAir.moveSpeed = 1;
        }
    }
}
