using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(ChaseInAir))]
//[RequireComponent(typeof(OnDeathDrop))]
[RequireComponent(typeof(EnemyAudioManager))]
public class FlyerThief : Enemy {

    [SerializeField]
    private EnemyBody body;

    [SerializeField]
    private float killValue;

    [SerializeField]
    private Transform[] spawns;

    private EnemyAudioManager audioManager;

    [SerializeField]
    private float deathDelayTime;

    private Boolean chasingPlayer;

    private ChaseInAir chaseInAir;

    void Start () { 
        chaseInAir = GetComponent<ChaseInAir>();
        chaseInAir.target = GameManager.instance.player.transform;
        chasingPlayer = true;
        audioManager = GetComponent<EnemyAudioManager>();
	}

    public Transform ChooseSpawn()
    {
        System.Random rand = new System.Random();
        int r = rand.Next(spawns.Length);
        return spawns[r];
    }

    public override void TakeDamage(int damage)
    {
        audioManager.playHurtSound();
        health -= damage;
        if(health <= 0)
        {
            audioManager.playDeathSound();
            Invoke("Die", deathDelayTime);
        }
    }

    public override void Die()
    { 
        base.Die();
    }

    public void SwitchTargets()
    {
        if (chasingPlayer)
        {
            chasingPlayer = false;
            chaseInAir.target = ChooseSpawn();
        }
        else
        {
            chasingPlayer = true;
            chaseInAir.target = GameManager.instance.player.transform;
        }
    }
}
