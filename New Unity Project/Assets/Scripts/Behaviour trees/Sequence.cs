using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : NodeBT
{
    protected List<NodeBT> nodes = new List<NodeBT>();

    public Sequence(List<NodeBT> _nodes)
    {
        nodes = _nodes;
    }

    public override NodeState Evaluate()
    {
        bool isAnyNodeRunning = false;

        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    isAnyNodeRunning = true;
                    node.Evaluate();
                    break;
                case NodeState.Ok:
                    break;
                case NodeState.Fail:
                    state = NodeState.Fail;
                    return state;
                default:
                    break;
            }
        }
        state = isAnyNodeRunning ? NodeState.Running : NodeState.Ok;
        return state;
    }
}
