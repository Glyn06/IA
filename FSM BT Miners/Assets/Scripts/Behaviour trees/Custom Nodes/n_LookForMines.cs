using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_LookForMines : NodeBT
{

    private Seeker seeker;
    private MinerBT miner;

    public n_LookForMines(Seeker _seeker, MinerBT _miner)
    {
        seeker = _seeker;
        miner = _miner;
    }

    public override NodeState Evaluate()
    {
        Mine gm = miner.LookForGoldMine();

        if (gm != null)
        {
            miner.SetGoldMine(gm);

            seeker.FindPath(miner.gameObject, miner.GetGoldMine().gameObject);
            return NodeState.Ok;
        }
        return NodeState.Fail;
    }
}
