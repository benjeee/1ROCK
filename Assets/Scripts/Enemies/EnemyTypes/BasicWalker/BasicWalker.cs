using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ChaseOnGround))]
[RequireComponent(typeof(ShootObjectForward))]
[RequireComponent(typeof(OnDeathDrop))]
[RequireComponent(typeof(EnemyAudioManager))]
public class BasicWalker : Enemy {

    [SerializeField]
    private EnemyBody body;

    [SerializeField]
    private EnemyAppendage lLeg;

    [SerializeField]
    private EnemyAppendage rLeg;

    private int numAppendages;

    [SerializeField]
    private float killValue;

    private ChaseOnGround chaseOnGround;
    private ShootObjectForward shootObjectForward;
    private OnDeathDrop onDeathDrop;
    private EnemyAudioManager audioManager;

    [SerializeField]
    private AudioClip hurtSound;

    [SerializeField]
    private AudioClip dieSound;

    [SerializeField]
    private float stunTime = 1.0f;

    [SerializeField]
    private AudioSource approachAudioSource;

    void Start()
    {
        Vector3 currPos = this.transform.position;
        this.transform.position = new Vector3(currPos.x, currPos.y + 2.2f, currPos.z);

        numAppendages = 2;
        body.parent = this;
        lLeg.parent = this;
        rLeg.parent = this;

        audioManager = GetComponent<EnemyAudioManager>();

        chaseOnGround = GetComponent<ChaseOnGround>();
        chaseOnGround.moveSpeed = 5;
        chaseOnGround.rotateSpeed = 3;
        chaseOnGround.StartMoving();
        chaseOnGround.StartRotating();
        chaseOnGround.target = GameManager.instance.player.transform;
        

        shootObjectForward = GetComponent<ShootObjectForward>();
        shootObjectForward.enabled = false;

        onDeathDrop = GetComponent<OnDeathDrop>();
    }

    public override void NotifyAppendageDestroyed(EnemyAppendage appendage)
    {
        numAppendages--;
        if(numAppendages == 0)
        {
            approachAudioSource.enabled = false;
            Vector3 currPos = this.transform.position;
            //TODO: animation for this
            this.transform.position = new Vector3(currPos.x, currPos.y - 1.2f, currPos.z);
            chaseOnGround.StopMoving();
            shootObjectForward.enabled = true;
        } else
        {
            chaseOnGround.moveSpeed = 2;
        }
    }

    private void Stun()
    {
        chaseOnGround.StopAll();
        chaseOnGround.Invoke("StartAll", stunTime);
    }

    public override void TakeDamage(int amt)
    {
        audioManager.playHurtSound();
        Stun();
        health-=amt;
        if (health <= 0)
        {
            audioManager.playDeathSound();
            Invoke("Die", damageDisplayTime);
        }
    }

    public override void Die()
    {
        onDeathDrop.DropItems();
        base.Die();
    }
}
