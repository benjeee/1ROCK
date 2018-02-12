using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseInAir : MonoBehaviour
{

    public Transform target;

    public float rotateSpeed;

    public float moveSpeed;

    private bool canMove = true;
    private bool canRotate = true;

    void Start()
    {
        target = GameManager.instance.player.transform;
    }

    void Update()
    {
        RotateTowardsTarget();
        MoveTowardsTarget();
    }

    void RotateTowardsTarget()
    {
        if (canRotate)
        {
            Vector3 targetDir = target.position - transform.position;
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    void MoveTowardsTarget()
    {
        if (canMove)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, target.position.z), step);
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
}
