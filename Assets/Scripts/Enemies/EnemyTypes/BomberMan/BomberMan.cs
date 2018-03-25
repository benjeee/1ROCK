using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ChaseOnGround))]
[RequireComponent(typeof(EnemyAudioManager))]
public class BomberMan : Enemy {

    ChaseOnGround chase;

    EnemyAudioManager audioManager;

    public bool canTakeDamage;

    void Start () {
        canTakeDamage = true;

        Vector3 currPos = this.transform.position;
        this.transform.position = new Vector3(currPos.x, currPos.y + 2.7f, currPos.z);

        chase = GetComponent<ChaseOnGround>();
        chase.target = GameManager.instance.player.transform;
        chase.StartMoving();
        chase.StartRotating();

        audioManager = GetComponent<EnemyAudioManager>();
	}

    public void Explode()
    {
        canTakeDamage = false;
        audioManager.playDeathSound();
        Instantiate(ResourceManager.instance.BasicExplosionPrefab, transform.position, transform.rotation);
        Invoke("Die", damageDisplayTime);
    }

    public override void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            audioManager.playHurtSound();
            health -= damage;
            if (health <= 0)
            {
                Explode();
            }
        }
    }
}
