using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ChaseInAir))]
public class Rock : MonoBehaviour{

    public int damage = 1;

    [SerializeField]
    private ChaseInAir rangedPickup;

    [SerializeField]
    private float pickupDistance;

    [SerializeField]
    private float rangedPickupBuffer = 1.0f;

    private float timeSinceThrown;

    void Start()
    {
        timeSinceThrown = 0.0f;
        rangedPickup = GetComponent<ChaseInAir>();
        rangedPickup.target = GameManager.instance.player.transform;
        rangedPickup.enabled = false;
    }

    void Update()
    {
        timeSinceThrown += Time.deltaTime;
        if(timeSinceThrown > rangedPickupBuffer &&
            (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) < pickupDistance))
        {
            rangedPickup.enabled = true;
        }
    }
}
