using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour {


    public static int FORWARD = 0;
    public static int UP = 1;


    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    public int damageValue;

    private bool canMove;

    [SerializeField]
    private int dir;

    void Start()
    {
        canMove = true;
    }
	
	public void Update () {
        if (canMove)
        {
            if(dir == FORWARD)
            {
                transform.position += transform.forward * projectileSpeed * Time.deltaTime;
            } else if(dir == UP)
            {
                transform.position += transform.up * projectileSpeed * Time.deltaTime;
            }
        }
	}

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

}
