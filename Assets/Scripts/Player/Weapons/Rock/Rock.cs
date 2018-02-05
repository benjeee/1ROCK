using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ChaseInAir))]
public class Rock : MonoBehaviour{

    public int damage = 1;

    [SerializeField]
    private ChaseInAir rangedPickup;

    void Start()
    {
        rangedPickup = GetComponent<ChaseInAir>();
        rangedPickup.target = GameManager.instance.player.transform;
        rangedPickup.enabled = false;
    }


    public void EnabledRangedPickup()
    {
        rangedPickup.enabled = true;
    }
}
