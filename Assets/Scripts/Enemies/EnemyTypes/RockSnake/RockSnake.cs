using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSnake : Enemy {

    public RandomPassthroughMovement movement;

    public RockSnakeHead head;

    [SerializeField]
    int numAppendages;

	new void Start () {
        movement = GetComponent<RandomPassthroughMovement>();
        health = 999999;
	}
}
