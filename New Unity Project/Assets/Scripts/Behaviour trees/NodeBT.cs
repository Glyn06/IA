using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Ok,
    Running,
    Fail
}

[System.Serializable]
public abstract class NodeBT
{
    protected NodeState state;

    public abstract NodeState Evaluate();

    public NodeState GetState() { return state; }
}
