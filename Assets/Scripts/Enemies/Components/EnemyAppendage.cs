using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppendage : EnemyPart {

    public int health;

    public override void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Invoke("DestroyAppendage", damageDisplayTime);
        }
        renderer.material = damageTakenMaterial;
        timeSinceDamageTaken = 0;
        parent.TakeDamage(dmg);
    }

    private void DestroyAppendage()
    {
        parent.NotifyAppendageDestroyed(this);
        Destroy(this.gameObject);
    }
}
