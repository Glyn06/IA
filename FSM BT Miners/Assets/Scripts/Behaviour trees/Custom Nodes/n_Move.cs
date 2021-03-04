using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_Move : NodeBT
{

    private Seeker seeker;

    public n_Move(Seeker _seeker)
    {
        seeker = _seeker;
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
