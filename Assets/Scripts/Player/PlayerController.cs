using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	private float speed = 12f;
	[SerializeField]
	private float sens = 2.5f;

	private PlayerMotor motor;
    private PlayerAttack attack;
    private Player player;

	void Start()
	{
		Cursor.visible = false;
		motor = GetComponent<PlayerMotor> ();
        attack = GetComponent<PlayerAttack>();
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
            attack.Attack();
        }
    }

    void HandleEatRock()
    {
        if (Input.GetButtonDown("Fire2") && attack.equipped == PlayerAttack.ROCK)
        {
            player.EatRock();
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
            attack.SwapWeapon(PlayerAttack.FIREBALL);
        } else if (Input.GetKeyDown("4"))
        {
            attack.SwapWeapon(PlayerAttack.SPEAR);
        } else if (Input.GetKeyDown("5"))
        {
            attack.SwapWeapon(PlayerAttack.BASKETBALL);
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
