using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadBlasterArm : EnemyAppendage {

    [SerializeField]
    QuadBlasterBlaster blasterChild;


    public override void DestroyAppendage()
    {
        if(blasterChild != null)
        {
            blasterChild.DestroyAppendage();
        }
        base.DestroyAppendage();
    }

    public void DestroyBlaster()
    {
        blasterChild = null;
    }
}
