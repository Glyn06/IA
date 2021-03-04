using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_Deposit : NodeBT {

    private MinerBT miner;

    public n_Deposit(MinerBT _miner)
    {
        miner = _miner;
    }

    public override NodeState Evaluate()
    {
        if (miner.home != null)
            miner.Deposit();

        if (miner.GetGoldDeposit() == 0)
            return NodeState.Ok;
        else
            return NodeState.Running;
    }
}
