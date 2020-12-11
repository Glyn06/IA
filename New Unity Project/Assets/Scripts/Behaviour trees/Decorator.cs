using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : NodeWithChildren
{

    public override void OnExecute()
    {
        if (children.Count > 1)
            Debug.LogWarning("Decorator node should only have one child");

        children[0].OnExecute();
        if (children[0].state == NodeState.Ok)
            state = NodeState.Fail;
        else if (children[0].state == NodeState.Fail)
            state = NodeState.Ok;
    }
}
