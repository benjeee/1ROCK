using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShootProjectileAtPlayer : MonoBehaviour {

    [SerializeField]
    private Transform projectilePrefab;

    [SerializeField]
    private float reloadSpeed = 1.0f;

    [SerializeField]
    private float spawnDistance = 1.0f;

    [SerializeField]
    private AudioClip shootSound;

    private AudioSource audioSource;

    private float timeSinceLastShot;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > reloadSpeed)
        {
            ShootObject();
            timeSinceLastShot = 0;
        }
    }

    private void ShootObject()
    {
        audioSource.PlayOneShot(shootSound);
        Vector3 relativePos = GameManager.instance.player.transform.position - transform.position;
        Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(relativePos));
    }
}
