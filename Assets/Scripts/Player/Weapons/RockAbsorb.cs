using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAbsorb : MonoBehaviour
{
    void Start()
    {
        this.enabled = false;
    }

    public void Absorb()
    {
        this.enabled = true;
        GameObject[] rockObjects = GameObject.FindGameObjectsWithTag("Rock");
        foreach (GameObject rockObject in rockObjects)
        {
            Rock rock = rockObject.GetComponent<Rock>();
            rock.rangedPickup.moveSpeed = 70;
            rock.rangedPickup.rotateSpeed = 20;
            rock.EnableRangedPickup();
        }
        this.enabled = false;
    }
}