using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : BasicProjectile {

    [SerializeField]
    private int impactDamage = 2;

    [SerializeField]
    private int impaleDOT = 1;

    [SerializeField]
    private float tickRate = 1;

    private float timeSinceLastTick;

    private EnemyPart impaledEnemyPart;

    new void Update()
    {
        base.Update();
        if(impaledEnemyPart != null)
        {
            timeSinceLastTick += Time.deltaTime;
            if(timeSinceLastTick >= tickRate)
            {
                impaledEnemyPart.TakeDamage(impaleDOT);
                timeSinceLastTick = 0;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(impaledEnemyPart == null)
        {
            if (col.gameObject.CompareTag("EnemyBody") || col.gameObject.CompareTag("EnemyAppendage"))
            {
                impaledEnemyPart = col.GetComponent<EnemyPart>();
                impaledEnemyPart.OnCollisionSlow();
                impaledEnemyPart.TakeDamage(impactDamage);
                this.transform.parent = impaledEnemyPart.transform;
                DisableMovement();
            }
        }
    }
}
