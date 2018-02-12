using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    private Animator animator;

    public int damage;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Swing()
    {
        animator.SetTrigger("BaseAttack1");
    }

    
}
