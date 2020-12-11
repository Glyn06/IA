using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Conditional : NodeBT {
    public abstract bool Condition();

    public override void OnExecute()
    {
        if (Condition())
            state = NodeState.Ok;
        else
            state = NodeState.Fail;
    }
}
