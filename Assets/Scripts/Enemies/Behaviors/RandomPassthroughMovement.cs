using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPassthroughMovement : MonoBehaviour {

    public Vector3 currTarget;
    public Vector3 groundTarget;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float rotateSpeed;

	void Start () {
        moveSpeed = 20;
        rotateSpeed = 5;
        groundTarget = new Vector3(0, 0, 0);
    }
	
	void Update () {
        if(currTarget == null)
        {
            currTarget = GetNewTarget();
        }
        if (ReachedTarget())
        {
            transform.position = GetNewStartPoint();
            currTarget = GetNewTarget();
        } else
        {
            RotateTowardsTarget();
            MoveTowardsTarget();
        }
	}

    Vector3 GetNewStartPoint()
    {
        return Random.onUnitSphere * 150;
    }

    Vector3 GetNewTarget()
    {
        //for player-directed movement, add line here to make groundTarget the current position of player
        Vector3 diffNormalized = ((groundTarget - transform.position) / transform.position.magnitude);
        return diffNormalized * 130f;
    }

    bool ReachedTarget()
    {
        float dist = Vector3.Distance(transform.position, currTarget);
        if(dist < 3f)
        {
            return true;
        }
        return false;
    }

    void RotateTowardsTarget()
    {
            Vector3 targetDir = currTarget - transform.position;
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
    }

    void MoveTowardsTarget()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(currTarget.x, currTarget.y, currTarget.z), step);
    }
}
