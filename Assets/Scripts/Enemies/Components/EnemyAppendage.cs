using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppendage : EnemyPart {

    public int health;

    public override void TakeDamage(int dmg)
    {
        int prevHealth = health;
        health -= dmg;
        renderer.material = damageTakenMaterial;
        timeSinceDamageTaken = 0;
        if (health <= 0)
        {
            Invoke("DestroyAppendage", damageDisplayTime);
        }
        parent.TakeDamage(Mathf.Min(prevHealth, dmg));
    }

    public virtual void DestroyAppendage()
    {
        parent.NotifyAppendageDestroyed(this);
        Destroy(this.gameObject);
    }
}
