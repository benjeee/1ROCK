using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	private float speed = 12f;
	[SerializeField]
	private float sens = 2.5f;

	private PlayerMotor motor;
    private PlayerAttack attack;

    [SerializeField]
    private float dashCooldown;
    private float timeSinceLastDash;

	void Start()
	{
		Cursor.visible = false;
		motor = GetComponent<PlayerMotor> ();
        attack = GetComponent<PlayerAttack>();
	}

    void HandleMovement()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;
        motor.Move(velocity);   

        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 playerRotation = new Vector3(0f, yRot, 0f) * sens;
        motor.Rotate(playerRotation);

        float camRotation = xRot * sens;
        motor.CamRotate(camRotation);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            motor.Jump();
        }
    }

    //screen zoom/lean effects here
    void HandleDash()
    {
        timeSinceLastDash += Time.deltaTime;
        if (timeSinceLastDash < dashCooldown) return;

        if (Input.GetButtonDown("Fire2"))
        {
            float xMov = Input.GetAxisRaw("Horizontal");
            float zMov = Input.GetAxisRaw("Vertical");
            int dir = 0;
            if(zMov > 0)
            {
                if(xMov > 0) dir = PlayerMotor.FORWARD_RIGHT;
                else if(xMov < 0) dir = PlayerMotor.FORWARD_LEFT;
                else dir = PlayerMotor.FORWARD;
            } else if(zMov < 0)
            {
                if (xMov > 0) dir = PlayerMotor.BACKWARD_RIGHT;
                else if (xMov < 0) dir = PlayerMotor.BACKWARD_LEFT;
                else dir = PlayerMotor.BACKWARD;
            } else
            {
                if(xMov > 0) dir = PlayerMotor.RIGHT;
                else if(xMov < 0) dir = PlayerMotor.LEFT;
                else dir = PlayerMotor.FORWARD;
            }
            motor.Dash(dir);
            timeSinceLastDash = 0;
        }
    }

    void HandleThrow()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            attack.Attack();
        }
    }

    void HandleScrollEquip()
    {
        float f = Input.GetAxis("Mouse ScrollWheel");
        if(f < 0)
        {
            attack.ShiftWeapon(-1);
        }else if(f > 0)
        {
            attack.ShiftWeapon(1);
        }
    }

    void HandleStopThrowing()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            attack.CancelThrowRock();
        }
    }

    void HandleSwapEquip()
    {
        if (Input.GetKeyDown("1"))
        {
            attack.SwapWeapon(PlayerAttack.SWORD);
        }
        else if (Input.GetKeyDown("2"))
        {
            attack.SwapWeapon(PlayerAttack.ROCK);
        } else if (Input.GetKeyDown("3"))
        {
            attack.SwapWeapon(PlayerAttack.SPEAR);
        } /*else if (Input.GetKeyDown("4"))
        {
            attack.SwapWeapon(PlayerAttack.BASKETBALL);
        }*/
    }

    void HandleFireball()
    {
        if (Input.GetKeyDown("f"))
        {
            attack.ThrowFireball();
        }
    }

    void HandleRockAbsorb()
    {
        if (Input.GetKeyDown("q"))
        {
            attack.AbsorbRocks();
        }
    }


    void Update()
	{
        HandleSwapEquip();
        HandleScrollEquip();
        HandleJump();
        HandleThrow();
        HandleFireball();
        HandleMovement();
        HandleDash();
        HandleRockAbsorb();
        HandleStopThrowing();
    }
}
