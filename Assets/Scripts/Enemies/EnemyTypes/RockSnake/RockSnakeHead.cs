using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSnakeHead : EnemyAppendage {

    //[SerializeField]
    public RockSnakeHead parentHead;

    //[SerializeField]
    public RockSnakeHead childHead;

    RockSnake parentRockSnake;

    new void Start()
    {
        base.Start();
        parentRockSnake = parent.GetComponent<RockSnake>();
    }

    void RegisterParentDead()
    {
        GameObject newRockSnake = new GameObject();
        newRockSnake.transform.position = transform.position;
        newRockSnake.transform.rotation = transform.rotation;
        RandomPassthroughMovement movement = newRockSnake.AddComponent<RandomPassthroughMovement>();
        RockSnake newRockSnakeObject = newRockSnake.AddComponent<RockSnake>();
        movement.currTarget = parentRockSnake.movement.currTarget;
        this.transform.parent = newRockSnake.transform;
        SetNewParentEnemy(newRockSnakeObject);
    }

    void SetNewParentEnemy(RockSnake rockSnake)
    {
        this.parent = rockSnake;
        if(childHead != null)
        {
            childHead.SetNewParentEnemy(rockSnake);
            rockSnake.head = childHead;
        }
    }

    public override void DestroyAppendage()
    {
        if(childHead != null)
        {
            childHead.RegisterParentDead();
        }
        base.DestroyAppendage();
    }
}
