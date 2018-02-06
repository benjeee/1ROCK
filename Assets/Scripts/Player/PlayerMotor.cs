﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    public static int FORWARD = 0;
    public static int FORWARD_RIGHT = 1;
    public static int FORWARD_LEFT = 2;
    public static int RIGHT = 3;
    public static int LEFT = 4;
    public static int BACKWARD = 5;
    public static int BACKWARD_RIGHT = 6;
    public static int BACKWARD_LEFT = 7;

    [SerializeField]
	private Camera cam;

    [SerializeField]
    private float maxCamRotation = 85f;

    [SerializeField]
    private float jumpSpeed = 5.0f;

    private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;

	private float camRotation = 0;
    private float currCamRotation = 0;
    
    private bool inJump = false;
	private Rigidbody rb;

    [SerializeField]
    private float dashForce = 20.0f;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	public void CamRotate(float _camRotation)
	{
		camRotation = _camRotation;
	}

	//run every physics tick
	void FixedUpdate()
	{
		PerformMovement ();
		PerformRotation ();
	}

	private void PerformMovement()
	{
		if (velocity != Vector3.zero) 
		{
			rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
		}
	}

	private void PerformRotation()
	{
		rb.MoveRotation (rb.rotation * Quaternion.Euler (rotation));
		if (cam != null) 
		{
            currCamRotation -= camRotation;
            currCamRotation = Mathf.Clamp(currCamRotation, -maxCamRotation, maxCamRotation);
            cam.transform.localEulerAngles = new Vector3(currCamRotation, 0f, 0f);
		}
	}

    public void Jump()
    {
        if (!inJump)
        {
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
        }
        inJump = true;
    }

    public void Dash(int mode)
    {
        if(mode == FORWARD)
        {
            rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        }
        else if(mode == FORWARD_RIGHT)
        {
            rb.AddForce((transform.forward + transform.right).normalized * dashForce, ForceMode.Impulse);
        }
        else if (mode == FORWARD_LEFT)
        {
            rb.AddForce((transform.forward - transform.right).normalized * dashForce, ForceMode.Impulse);
        }
        else if (mode == RIGHT)
        {
            rb.AddForce(transform.right * dashForce, ForceMode.Impulse);
        }
        else if (mode == LEFT)
        {
            rb.AddForce(-transform.right * dashForce, ForceMode.Impulse);
        }
        else if (mode == BACKWARD)
        {
            rb.AddForce(-transform.forward * dashForce, ForceMode.Impulse);
        }
        else if (mode == BACKWARD_LEFT)
        {
            rb.AddForce((-transform.forward - transform.right).normalized * dashForce, ForceMode.Impulse);
        }
        else if (mode == BACKWARD_RIGHT)
        {
            rb.AddForce((-transform.forward + transform.right).normalized * dashForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        inJump = false;
    }
}
