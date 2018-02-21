using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ChaseOnGround))]
public class BomberMan : Enemy {

    ChaseOnGround chase;

	void Start () {

        Vector3 currPos = this.transform.position;
        this.transform.position = new Vector3(currPos.x, currPos.y + 2.7f, currPos.z);

        chase = GetComponent<ChaseOnGround>();
        chase.target = GameManager.instance.player.transform;
        chase.StartMoving();
        chase.StartRotating();
	}

    public void Explode()
    {
        Instantiate(ResourceManager.instance.BasicExplosionPrefab, transform.position, transform.rotation);
    }

    public override void Die()
    {
        Explode();
        base.Die();
    }

}
