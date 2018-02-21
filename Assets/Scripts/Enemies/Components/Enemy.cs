using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour{

    public int health;

    public float damageDisplayTime = 0.2f;

    public Wave wave;

    public virtual void TakeDamage(int amt)
    {
        health -= amt;
        if(health <= 0)
        {
            Invoke("Die", damageDisplayTime);
        }
    }

    public virtual void Die()
    {
        wave.RegisterEnemyDead(this);
        Destroy(this.gameObject);
    }

    public virtual void NotifyAppendageDestroyed(EnemyAppendage appendage)
    {

    }
}
