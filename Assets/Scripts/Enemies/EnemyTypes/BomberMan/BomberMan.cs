using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ChaseOnGround))]
public class BomberMan : Enemy {

    ChaseOnGround chase;

	new void Start () {
        chase = GetComponent<ChaseOnGround>();
        chase.target = GameManager.instance.player.transform;
        chase.StartMoving();
        chase.StartRotating();
	}

    public void Explode()
    {
        //instantiate some explosion prefab (trigger sphere collider + particle system + script that detects player collision and deals damage)
        Debug.Log("EXPLOOODDEEE!!!");
    }

    public override void Die()
    {
        Explode();
        base.Die();
    }

}
