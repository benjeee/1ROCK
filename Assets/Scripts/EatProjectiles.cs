using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatProjectiles : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Projectile") || col.gameObject.CompareTag("Fireball"))
        {
            Destroy(col.gameObject);
        }
    }
}
