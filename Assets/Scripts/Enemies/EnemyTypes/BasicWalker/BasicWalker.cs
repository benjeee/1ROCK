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

    // Use this for initialization
    void Start()
    {
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

    IEnumerator Stun()
    {
        chaseOnGround.enabled = false;
        yield return new WaitForSeconds(stunTime);
        chaseOnGround.enabled = true;
    }

    public override void TakeDamage(int amt)
    {
        GameManager.instance.SlowForSeconds(0.02f);
        audioManager.playHurtSound();
        StartCoroutine(Stun());
        health-=amt;
        if (health <= 0)
        {
            StartCoroutine(DelayDeath());
        }
    }

    IEnumerator DelayDeath()
    {
        audioManager.playDeathSound();
        yield return new WaitForSeconds(damageDisplayTime);
        Die();
    }

    public override void Die()
    {
        onDeathDrop.DropItems();
        Destroy(this.gameObject);
    }
}
