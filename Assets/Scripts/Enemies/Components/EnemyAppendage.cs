using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppendage : MonoBehaviour {

    public int health;
    public Enemy parent;

    [SerializeField]
    private Material baseMaterial;

    [SerializeField]
    private Material damageTakenMaterial;

    [SerializeField]
    private int damageValue;

    [SerializeField]
    private float knockbackValue;

    private Renderer appendageRenderer;

    private float damageDisplayTime = 0.2f;
    private float timeSinceDamageTaken = 0.0f;

    public void Start()
    {
        appendageRenderer = GetComponent<Renderer>();
    }

    public void Update()
    {
        timeSinceDamageTaken = timeSinceDamageTaken + Time.deltaTime;
        if (timeSinceDamageTaken > damageDisplayTime)
        {
            appendageRenderer.material = baseMaterial;
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            StartCoroutine(DelayDestroy());
        }
        appendageRenderer.material = damageTakenMaterial;
        timeSinceDamageTaken = 0;
        parent.TakeDamage(dmg);
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(damageDisplayTime);
        DestroyAppendage();
    }

    private void DestroyAppendage()
    {
        parent.NotifyAppendageDestroyed(this);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            Rock r = col.gameObject.GetComponent<Rock>();
            TakeDamage(r.damage);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            GameManager.instance.player.TakeDamage(damageValue);
            GameManager.instance.player.KnockBack(col, knockbackValue);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Fireball"))
        {
            BasicProjectile bp = col.gameObject.GetComponent<BasicProjectile>();
            TakeDamage(bp.damageValue);
        } else if (col.gameObject.CompareTag("Sword"))
        {
            TakeDamage(1);
        }
    }
}
