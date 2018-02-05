using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupField : MonoBehaviour {

    private float timeSinceThrown;

    private bool activated;

    [SerializeField]
    private Rock parent;

	// Use this for initialization
	void Start () {
        timeSinceThrown = 0f;
        activated = false;
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceThrown += Time.deltaTime;
        if(timeSinceThrown >= 1.0f)
        {
            activated = true;
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && activated)
        {
            parent.EnabledRangedPickup();
        }
    }
}
