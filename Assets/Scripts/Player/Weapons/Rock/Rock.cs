using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ChaseInAir))]
public class Rock : MonoBehaviour{

    public int damage = 1;

    public ChaseInAir rangedPickup;

    public bool pickupFieldActivated;
    private float timeSinceThrown;

    [SerializeField]
    private Collider pickupTrigger;

    void Start()
    {
        timeSinceThrown = 0f;
        pickupFieldActivated = false;

        rangedPickup = GetComponent<ChaseInAir>();
        rangedPickup.target = GameManager.instance.player.transform;
        rangedPickup.enabled = false;
    }

    void Update()
    {
        timeSinceThrown += Time.deltaTime;
        if (timeSinceThrown >= 0.1f)
        {
            pickupFieldActivated = true;
        }
    }


    public void EnableRangedPickup()
    {
        pickupTrigger.enabled = true;
        rangedPickup.enabled = true;
    }

    public void DisableRangedPickup()
    {
        rangedPickup.enabled = true;
    }
}
