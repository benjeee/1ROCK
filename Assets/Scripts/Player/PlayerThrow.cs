using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerThrow : MonoBehaviour
{
    public static int ROCK = 1;
    public static int FIREBALL = 2;
    public static int SPEAR = 3;
    public static int BASKETBALL = 4;

    public int equipped = ROCK;

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
    private Transform rockShooter;

    [SerializeField]
    private Player player;

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
    }

    public void ShiftWeapon(int dir)
    {
        if(dir == 1)
        {
            if (equipped == BASKETBALL) equipped = ROCK;
            else equipped++;
        }else
        {
            if (equipped == ROCK) equipped = BASKETBALL;
            else equipped--;
        }
        Debug.Log(equipped);
    }

    public void SwapWeapon(int val)
    {
        if(val == ROCK || val == FIREBALL || val == SPEAR || val == BASKETBALL)
        {
            equipped = val;
        }
    }

    public void Throw()
    {
        if(equipped == ROCK)
        {
            ThrowRock();
        } else if (equipped == FIREBALL)
        {
            ThrowFireball();
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

    private void ThrowFireball()
    {
        Instantiate(fireballPrefab, rockShooter.position, rockShooter.rotation);
    }
}