using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerAttack : MonoBehaviour
{

    public static int SWORD = 1;
    public static int ROCK = 2;
    public static int SPEAR = 3;
    //public static int BASKETBALL = 4;

    public int equipped = ROCK;

    private bool throwingRocks;

    [SerializeField]
    private float throwSpeed = 0.4f;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private float throwForce = 150f;

    [SerializeField]
    private Transform rockPrefab;

    [SerializeField]
    private Transform fireballPrefab;

    [SerializeField]
    private Transform spearPrefab;

    [SerializeField]
    private Transform rockShooter;

    [SerializeField]
    private Player player;

    [SerializeField]
    private Sword sword;

    [SerializeField]
    private RockAbsorb rockAbsorb;

    [SerializeField]
    private AudioClip throwSound;

    private AudioSource audioSource;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("No camera referenced!");
            this.enabled = false;
        }
        audioSource = GetComponent<AudioSource>();
        throwingRocks = false;
    }

    public void ShiftWeapon(int dir)
    {
        CancelThrowRock();
        if (dir == 1)
        {
            if (equipped == SPEAR) equipped = SWORD;
            else equipped++;
        }else
        {
            if (equipped == SWORD) equipped = SPEAR;
            else equipped--;
        }
        UIController.instance.ChangeEquipIndicator(equipped);
    }

    public void SwapWeapon(int val)
    {
        CancelThrowRock();
        if (val == SWORD || val == ROCK || val == SPEAR)
        {
            equipped = val;
            UIController.instance.ChangeEquipIndicator(equipped);
        }
    }

    public void Attack()
    {
        if(equipped == SWORD)
        {
            sword.Swing();
        }
        else if(equipped == ROCK)
        {
            throwingRocks = true;
            InvokeRepeating("ThrowRock", 0f, throwSpeed);
        }
        else if (equipped == SPEAR)
        {
            ThrowSpear();
        }
    }

    public void CancelThrowRock()
    {
        if (throwingRocks)
        {
            CancelInvoke();
            throwingRocks = false;
        }
    }

    private void ThrowRock()
    {
        if (player.numRocks > 0)
        {
            Transform rock = Instantiate(rockPrefab, rockShooter.position, rockShooter.rotation);
            rock.GetComponent<Rigidbody>().AddForce(rock.transform.forward * throwForce, ForceMode.Impulse);
            audioSource.clip = throwSound;
            audioSource.Play();
            player.DecrementRockCount();
        }
    }

    public void ThrowFireball()
    {
        if (GameManager.instance.player.SpendMana(50))
        {
            Instantiate(fireballPrefab, rockShooter.position, rockShooter.rotation);
        }
    }

    public void AbsorbRocks()
    {
        rockAbsorb.Absorb();
    }

    private void ThrowSpear()
    {
        if(player.numSpears > 0)
        {
            Transform spear = Instantiate(spearPrefab, rockShooter.position, rockShooter.rotation);
            spear.Rotate(90, 0, 0);
            player.DecrementSpearCount();
        }
    }


}