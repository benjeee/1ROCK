using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private int startingHealth = 10;

    [SerializeField]
    private int _numRocks = 10;
    public int numRocks
    {
        get { return _numRocks; }
    }

    [SerializeField]
    private AudioClip pickupSound;

    [SerializeField]
    private AudioClip playerHurt;

    private Rigidbody rb;

    private AudioSource audioSource;

    private int maxHealth;
    private int currHealth;

    private float timeUntilNextDamageWindow = 0;

    [SerializeField]
    private float damageBufferTime = 0.02f;

	// Use this for initialization
	void Start () {
        InitPlayer();
	}

    void InitPlayer()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        maxHealth = startingHealth;
        currHealth = startingHealth;
    }
	
	// Update is called once per frame
	void Update () {
		if(timeUntilNextDamageWindow > 0)
        {
            timeUntilNextDamageWindow = Math.Max(0, timeUntilNextDamageWindow - Time.deltaTime);
        }
	}

    public void TakeDamage(int damage)
    {
        if(timeUntilNextDamageWindow == 0)
        {
            GameManager.instance.SlowForSeconds(0.02f);
            audioSource.clip = playerHurt;
            audioSource.Play();
            currHealth -= damage;
            timeUntilNextDamageWindow = damageBufferTime;
            UIController.instance.ShowBlood(1.0f);
            StartCoroutine(ScreenShake(damage));
            if (currHealth <= 0)
            {
                Die();
            }
        }
    }

    IEnumerator ScreenShake(int intensity)
    {
        Vector2 shake;
        for(int i = 0; i < 10; i++)
        {
            shake = UnityEngine.Random.insideUnitCircle * intensity * .2f;
            cam.transform.localPosition = new Vector3(shake.x, shake.y, 0);
            yield return null;
        }
        ResetCamPosition();
    }

    void ResetCamPosition()
    {
        cam.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void HealDamage(int amt)
    {
        currHealth += amt;
        if(currHealth >= maxHealth)
        {
            currHealth = maxHealth;
        }
    }

    public void EatRock()
    {
        if(_numRocks <= 0)
        {
            return;
        }
        if(currHealth == maxHealth)
        {
            return;
        }
        HealDamage(1);
        DecrementRockCount();
    }

    public void DecrementRockCount()
    {
        if(_numRocks > 0)
        {
            _numRocks--;
            UIController.instance.UpdateRockCountText(numRocks);
        }
    }

    public void IncrementRockCount()
    {
        _numRocks++;
        UIController.instance.UpdateRockCountText(numRocks);
    }

    private void Die()
    {
        Debug.Log("Player DEAD!!!");
    }

    public void KnockBack(Collision col, float intensity)
    {
        rb.AddForce(col.transform.forward * -1 * intensity, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            audioSource.clip = pickupSound;
            audioSource.Play();
            IncrementRockCount();
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Projectile"))
        {
            BasicProjectile bp = col.gameObject.GetComponent<BasicProjectile>();
            TakeDamage(bp.damageValue);
            Destroy(col.gameObject);
        }
    }
}
