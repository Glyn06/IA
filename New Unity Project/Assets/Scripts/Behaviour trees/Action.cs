using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : NodeBT {
    public abstract NodeState Run();

    public override void OnExecute()
    {
        state = Run();
    }
}
