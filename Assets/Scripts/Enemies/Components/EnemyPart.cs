using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPart : MonoBehaviour {

    public Enemy parent;

    [SerializeField]
    protected Material baseMaterial;

    [SerializeField]
    protected Material damageTakenMaterial;

    [SerializeField]
    protected int damageValue;

    [SerializeField]
    protected float knockbackValue;

    protected float damageDisplayTime = 0.2f;
    protected float timeSinceDamageTaken = 0.0f;

    public Renderer renderer;

    public void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void Update()
    {
        timeSinceDamageTaken = timeSinceDamageTaken + Time.deltaTime;
        if (timeSinceDamageTaken > damageDisplayTime)
        {
            renderer.material = baseMaterial;
        }
    }

    public virtual void TakeDamage(int amt){}

    public void OnCollisionSlow()
    {
        GameManager.instance.SlowForSeconds(0.03f);
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            OnCollisionSlow();
            Rock r = col.gameObject.GetComponent<Rock>();
            TakeDamage(r.damage);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            OnCollisionSlow();
            GameManager.instance.player.TakeDamage(damageValue);
            GameManager.instance.player.KnockBack(col, knockbackValue);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Fireball"))
        {
            OnCollisionSlow();
            BasicProjectile bp = col.gameObject.GetComponent<BasicProjectile>();
            TakeDamage(bp.damageValue);
        }
        else if (col.gameObject.CompareTag("Sword"))
        {
            OnCollisionSlow();
            TakeDamage(GameManager.instance.player.sword.damage);
        }
    }
}
