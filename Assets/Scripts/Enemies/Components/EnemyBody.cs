using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour {

    public Enemy parent;

    [SerializeField]
    private Material baseMaterial;

    [SerializeField]
    private Material damageTakenMaterial;

    [SerializeField]
    private int damageValue;

    [SerializeField]
    private float knockbackValue;

    private float damageDisplayTime = 0.2f;
    private float timeSinceDamageTaken = 0.0f;

    private Renderer bodyRenderer;

    public void Start()
    {
        bodyRenderer = GetComponent<Renderer>();
    }

    public void Update()
    {
        timeSinceDamageTaken = timeSinceDamageTaken + Time.deltaTime;
        if (timeSinceDamageTaken > damageDisplayTime)
        {
            bodyRenderer.material = baseMaterial;
        }
    }

    public void TakeDamage(int dmg)
    {
        parent.TakeDamage(dmg);
        bodyRenderer.material = damageTakenMaterial;
        timeSinceDamageTaken = 0;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            Rock r = col.gameObject.GetComponent<Rock>();
            TakeDamage(r.damage);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hit player!");
            GameManager.instance.player.TakeDamage(damageValue);
            GameManager.instance.player.KnockBack(col, knockbackValue);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Fireball"))
        {
            BasicProjectile bp = col.gameObject.GetComponent<BasicProjectile>();
            TakeDamage(bp.damageValue);
        }
    }
}
