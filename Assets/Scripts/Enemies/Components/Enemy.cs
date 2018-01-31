using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public int health;

    public float damageDisplayTime = 0.2f;

    public virtual void TakeDamage(int amt)
    {
        GameManager.instance.SlowForSeconds(1f);
        health -= amt;
        if(health <= 0)
        {
            StartCoroutine(DelayDeath());
        }
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(damageDisplayTime);
        Die();
    }

    public virtual void Die()
    {
        Debug.Log("Enemy is dead!");
        Destroy(this.gameObject);
    }

    public virtual void NotifyAppendageDestroyed(EnemyAppendage appendage)
    {
        Debug.Log(appendage.transform.name + " was destroyed");
    }
}
