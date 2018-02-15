using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPassthroughMovement : MonoBehaviour {

    public Vector3 currTarget;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float rotateSpeed;

	// Use this for initialization
	void Start () {
        moveSpeed = 20;
        rotateSpeed = 5;
        //transform.position = GetNewStartPoint();
        //currTarget = GetNewTarget();
    }
	
	// Update is called once per frame
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
        //Vector2 centralPointOnSurface = Random.insideUnitCircle * 10;
        //Vector3 angleGenerationPoint = new Vector3(centralPointOnSurface.x, Random.Range(0f, 5f), centralPointOnSurface.y);
        //Vector3 angleGenerationPoint = new Vector3(0, 0, 0);
        //Vector3 diff = angleGenerationPoint - transform.position;
        Vector3 diffNormalized = -(transform.position / transform.position.magnitude);
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
