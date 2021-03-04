using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_LookForHome : NodeBT {

    private Seeker seeker;
    private MinerBT miner;

    public n_LookForHome(Seeker _seeker, MinerBT _miner)
    {
        seeker = _seeker;
        miner = _miner;
    }

    public override NodeState Evaluate()
    {
        if (miner.home != null)
        {
            seeker.FindPath(miner.gameObject, miner.home.gameObject);
            return NodeState.Ok;
        }

        return NodeState.Fail;
    }
}
