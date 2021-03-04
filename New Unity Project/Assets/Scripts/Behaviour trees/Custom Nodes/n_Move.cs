using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_Move : NodeBT
{

    private Seeker seeker;
    private MinerBT miner;

    public n_Move(Seeker _seeker, MinerBT _miner)
    {
        seeker = _seeker;
        miner = _miner;
    }

    public override NodeState Evaluate()
    {
        if (seeker.target != null)
            seeker.Move();

        if (seeker.gameObject.transform.position ==
            seeker.target.transform.position)
            return NodeState.Ok;
        else
            return NodeState.Running;
    }
}
