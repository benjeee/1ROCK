using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseOnGround : MonoBehaviour {

    public Transform target;

    public float rotateSpeed;

    public float moveSpeed;

    private bool canMove;
    private bool canRotate;
    
    void Update()
    {
        RotateTowardsPlayer();
        MoveTowardsPlayer();
    }
    
    void RotateTowardsPlayer()
    {
        if (canRotate)
        {
            Vector3 targetDir = target.position - transform.position;
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(targetDir.x, 0f, targetDir.z), step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    void MoveTowardsPlayer()
    {
        if (canMove)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), step);
        }
    }

    public void StartMoving()
    {
        canMove = true;
    }

    public void StartRotating()
    {
        canRotate = true;
    }

    public void StopMoving()
    {
        canMove = false;
    }

    public void StopRotating()
    {
        canRotate = false;
    }

    public void StopAll()
    {
        this.enabled = false;
    }

    public void StartAll()
    {
        this.enabled = true;
    }
}
