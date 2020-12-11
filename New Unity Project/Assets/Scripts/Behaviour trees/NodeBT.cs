using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Ok,
    Running,
    Fail
}

public class NodeBT : MonoBehaviour
{
    public NodeState state;

    public virtual void OnStart() { }
    public virtual void OnExecute(){ }
    public virtual void OnStop() { }
}
