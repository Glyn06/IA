using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : NodeWithChildren {
    public override void OnExecute()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].OnExecute();

            while (children[i].state == NodeState.Running)
            {
                children[i].OnExecute();
            }

            if (children[i].state == NodeState.Fail)
                i++;

            if (i == children.Count)
            {
                i = 0;
                state = children[i].state;
            }
        }
    }
}
