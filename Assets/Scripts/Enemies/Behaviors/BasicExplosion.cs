using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicExplosion : MonoBehaviour {

    [SerializeField]
    float knockbackValue;

    [SerializeField]
    float initialExplosionSize;

    [SerializeField]
    float maxExplosionSize;

    [SerializeField]
    int damageValue;
    public int DamageValue
    {
        get { return damageValue; }
    }

    [SerializeField]
    float lifetime;

    float timeAlive;

	void Start () {
        Invoke("DestroySelf", lifetime);
	}

    void Update()
    {
        timeAlive += Time.deltaTime / lifetime;
        transform.localScale = Vector3.Lerp(new Vector3(initialExplosionSize, initialExplosionSize, initialExplosionSize), new Vector3(maxExplosionSize, maxExplosionSize, maxExplosionSize), timeAlive);
    }
	
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.instance.player.TakeDamage(damageValue);
            GameManager.instance.player.KnockBack(col, knockbackValue);
        }
    }
}
