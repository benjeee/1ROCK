﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerAttack : MonoBehaviour
{

    public static int SWORD = 1;
    public static int ROCK = 2;
    public static int SPEAR = 3;
    public static int BASKETBALL = 4;

    public int equipped = ROCK;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private float throwForce = 150f;

    [SerializeField]
    private Transform rockPrefab;

    [SerializeField]
    private Transform fireballPrefab;

    [SerializeField]
    private Transform spearPrefab;

    [SerializeField]
    private Transform rockShooter;

    [SerializeField]
    private Player player;

    [SerializeField]
    private Sword sword;

    [SerializeField]
    private AudioClip throwSound;

    private AudioSource audioSource;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("No camera referenced!");
            this.enabled = false;
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void ShiftWeapon(int dir)
    {
        if(dir == 1)
        {
            if (equipped == BASKETBALL) equipped = ROCK;
            else equipped++;
        }else
        {
            if (equipped == ROCK) equipped = BASKETBALL;
            else equipped--;
        }
    }

    public void SwapWeapon(int val)
    {
        if(val == SWORD || val == ROCK || val == SPEAR || val == BASKETBALL)
        {
            equipped = val;
        }
    }

    public void Attack()
    {
        if(equipped == SWORD)
        {
            sword.Swing();
        }
        else if(equipped == ROCK)
        {
            ThrowRock();
        }
        else if (equipped == SPEAR)
        {
            ThrowSpear();
        }
    }

    private void ThrowRock()
    {
        if (player.numRocks > 0)
        {
            Transform rock = Instantiate(rockPrefab, rockShooter.position, rockShooter.rotation);
            rock.GetComponent<Rigidbody>().AddForce(rock.transform.forward * throwForce, ForceMode.Impulse);
            audioSource.clip = throwSound;
            audioSource.Play();
            player.DecrementRockCount();
        }
    }

    public void ThrowFireball()
    {
        if (GameManager.instance.player.SpendMana(50))
        {
            Instantiate(fireballPrefab, rockShooter.position, rockShooter.rotation);
        }
    }

    private void ThrowSpear()
    {
        Transform spear = Instantiate(spearPrefab, rockShooter.position, rockShooter.rotation);
        spear.Rotate(90, 0, 0);
    }
}