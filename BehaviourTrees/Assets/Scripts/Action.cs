using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : Node {
    public abstract NodeState Run();

    public override void OnExecute()
    {
        state = Run();
    }
}
