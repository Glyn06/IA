using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isInMine : Conditional {

    public override bool Condition()
    {
        return (gameObject.transform.position == gameObject.GetComponent<MinerBT>().GetGoldMine().transform.position);
    }
}
