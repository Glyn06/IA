using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isInHome : Conditional {

    public override bool Condition()
    {
        return (gameObject.transform.position == gameObject.GetComponent<MinerBT>().deposit.transform.position);
    }
}
