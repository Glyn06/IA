using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogicType
{
    And,
    Or
}

public abstract class Logic : NodeWithChildren
{
    protected void SetLogicType(LogicType logic) { logicType = logic; }
    LogicType logicType = LogicType.And;
    List<Node> succesList;

    public override void OnExecute()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].OnExecute();

            if (children[i].state == NodeState.Ok)
                succesList.Add(children[i]);
        }

        if (logicType == LogicType.And)
        {
            if (succesList.Count == children.Count)
                state = NodeState.Ok;
            else
                state = NodeState.Fail;
        }
        else if (logicType == LogicType.Or)
        {
            if (succesList.Count == 0)
                state = NodeState.Fail;
            else
                state = NodeState.Ok;
        }
    }
}
