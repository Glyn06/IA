using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasGold : Conditional {

    public override bool Condition()
    {
        return (gameObject.GetComponent<MinerBT>().GetGoldDeposit() >= gameObject.GetComponent<MinerBT>().maxGoldCapacity);
    }
}
