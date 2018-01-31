using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathDrop : MonoBehaviour {

    [SerializeField]
    private Transform ItemToDrop;

    [SerializeField]
    private int numItemsToDrop;

    public void DropItems()
    {
        for(int i = 0; i < numItemsToDrop; i++)
        {
            Transform item = Instantiate(ItemToDrop, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddForce(2 * Random.insideUnitSphere + transform.up, ForceMode.Impulse);
            }
        }
    }
}
