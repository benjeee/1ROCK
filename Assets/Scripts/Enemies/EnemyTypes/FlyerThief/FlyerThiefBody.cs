using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerThiefBody : EnemyBody {

    FlyerThief ftParent;

    new void Start()
    {
        base.Start();
        ftParent = (FlyerThief)parent;
    }

    new public void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
        if(col.gameObject.CompareTag("Player"))
        {
            ftParent.SwitchTargets();
            GameManager.instance.player.DecrementRockCount();
        }   
    }

    new public void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.gameObject.CompareTag("Spawn"))
        {
            ftParent.SwitchTargets();
        }
    }
}
