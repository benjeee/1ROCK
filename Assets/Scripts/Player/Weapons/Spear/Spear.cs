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

    private EnemyBody impaledEnemyBody;
    private EnemyAppendage impaledEnemyAppendage;

    new void Update()
    {
        base.Update();
        if(impaledEnemyBody != null)
        {
            timeSinceLastTick += Time.deltaTime;
            if(timeSinceLastTick >= tickRate)
            {
                impaledEnemyBody.TakeDamage(impaleDOT);
                timeSinceLastTick = 0;
            }
        } else if(impaledEnemyAppendage != null)
        {
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick >= tickRate)
            {
                impaledEnemyAppendage.TakeDamage(impaleDOT);
                timeSinceLastTick = 0;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collided with " + col.gameObject.name);
        if(impaledEnemyAppendage == null && impaledEnemyBody == null)
        {
            if (col.gameObject.CompareTag("EnemyBody"))
            {
                impaledEnemyBody = col.GetComponent<EnemyBody>();
                impaledEnemyBody.TakeDamage(impactDamage);
                this.transform.parent = impaledEnemyBody.transform;
                DisableMovement();
            } else if (col.gameObject.CompareTag("EnemyAppendage"))
            {
                impaledEnemyAppendage = col.GetComponent<EnemyAppendage>();
                impaledEnemyAppendage.TakeDamage(impactDamage);
                this.transform.parent = impaledEnemyAppendage.transform;
                DisableMovement();
            }
        }
    }
}
