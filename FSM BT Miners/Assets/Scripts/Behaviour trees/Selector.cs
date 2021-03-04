using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : NodeBT
{
    protected List<NodeBT> nodes = new List<NodeBT>();

    public Selector(List<NodeBT> _nodes)
    {
        nodes = _nodes;
    }
    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    state = NodeState.Running;
                    return state;
                case NodeState.Ok:
                    state = NodeState.Ok;
                    return state;
                case NodeState.Fail:
                    break;
                default:
                    break;
            }
        }
        state = NodeState.Fail;
        return state;
    }
}
