using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerThrow))]
[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	private float speed = 12f;
	[SerializeField]
	private float sens = 2.5f;

	private PlayerMotor motor;
    private PlayerThrow thrower;
    private Player player;

	void Start()
	{
		Debug.Log ("Started Controller");
		Cursor.visible = false;
		motor = GetComponent<PlayerMotor> ();
        thrower = GetComponent<PlayerThrow>();
        player = GetComponent<Player>();
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

    void HandleThrow()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            thrower.Throw();
        }
    }

    void HandleEatRock()
    {
        if (Input.GetButtonDown("Fire2") && thrower.equipped == PlayerThrow.ROCK)
        {
            player.EatRock();
        }
    }

    void HandleScrollEquip()
    {
        float f = Input.GetAxis("Mouse ScrollWheel");
        if(f < 0)
        {
            thrower.ShiftWeapon(-1);
        }else if(f > 0)
        {
            thrower.ShiftWeapon(1);
        }
    }

    void HandleSwapEquip()
    {
        if (Input.GetKeyDown("1"))
        {
            thrower.SwapWeapon(PlayerThrow.ROCK);
        } else if (Input.GetKeyDown("2"))
        {
            thrower.SwapWeapon(PlayerThrow.FIREBALL);
        } else if (Input.GetKeyDown("3"))
        {
            thrower.SwapWeapon(PlayerThrow.SPEAR);
        } else if (Input.GetKeyDown("4"))
        {
            thrower.SwapWeapon(PlayerThrow.BASKETBALL);
        }
    }


    void Update()
	{
        HandleSwapEquip();
        HandleScrollEquip();
        HandleEatRock();
        HandleThrow();
        HandleMovement();
        HandleJump();
	}
}
