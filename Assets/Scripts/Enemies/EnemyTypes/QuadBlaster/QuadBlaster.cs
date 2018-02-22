using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ChaseInAir))]
[RequireComponent(typeof(EnemyAudioManager))]
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

    EnemyAudioManager audioManager;

    int numBlasters;

    void Start()
    {
        qbUL.EnableShooting();
        qbUR.Invoke("EnableShooting", .25f);
        qbBR.Invoke("EnableShooting", .5f);
        qbBL.Invoke("EnableShooting", .75f);

        chaseInAir = GetComponent<ChaseInAir>();

        audioManager = GetComponent<EnemyAudioManager>();

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

    public override void TakeDamage(int damage)
    {
        audioManager.playHurtSound();
        health -= damage;
        if (health <= 0)
        {
            audioManager.playDeathSound();
            Invoke("Die", damageDisplayTime);
        }
    }
}
