using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_Mine : NodeBT {

    private MinerBT miner;

    public n_Mine(MinerBT _miner) {
        miner = _miner;
    }

    public override NodeState Evaluate()
    {
        if (miner.GetGoldMine() != null)
            miner.Mine();

        if (miner.GetGoldDeposit() >= miner.maxGoldCapacity)
            return NodeState.Ok;
        else
            return NodeState.Running;
    }
}
