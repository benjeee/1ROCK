using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSnake : Enemy {

    public RandomPassthroughMovement movement;

    public RockSnakeHead head;

    [SerializeField]
    int numAppendages;

	void Start () {
        movement = GetComponent<RandomPassthroughMovement>();
        health = 999999;
	}

    int CountRemainingChildren(RockSnakeHead currHead)
    {
        if (currHead == null) return 0;
        return 1 + CountRemainingChildren(currHead.childHead);
    }

    public override void NotifyAppendageDestroyed(EnemyAppendage appendage)
    {
        if(appendage == head)
        {
            Invoke("Die", damageDisplayTime);
        }
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }
}
