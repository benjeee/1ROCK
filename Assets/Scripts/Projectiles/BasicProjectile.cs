using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour {

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    public int damageValue;
	
	void Update () {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
	}
}
