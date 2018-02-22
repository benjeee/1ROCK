using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberManBody : EnemyBody {

    [SerializeField]
    BomberMan bomberManParent;

    public override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
        if (col.gameObject.CompareTag("Player"))
        {
            bomberManParent.Explode();
        }
    }

}
