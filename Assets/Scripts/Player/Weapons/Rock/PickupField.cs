using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupField : MonoBehaviour {

    [SerializeField]
    private Rock parent;	

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && parent.pickupFieldActivated)
        {
            parent.EnableRangedPickup();
        }
    }
}
