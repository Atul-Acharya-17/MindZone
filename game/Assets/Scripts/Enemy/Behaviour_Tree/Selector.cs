using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    /// <summary>
    /// Children nodes of the selector node
    /// </summary>
    private Node[] children;

    /// <summary>
    /// Parameterized constuctor
    /// </summary>
    /// <param name="nodes">List of children nodes</param>
    public Selector(params Node[] nodes)
    {
        children = nodes;
    }

    /// <summary>
    /// Executes the Selector node functionality
    /// </summary>
    /// <returns>State of the selector node</returns>
    public override NodeState Evaluate()
    {
        foreach (Node child in children)
        {
            switch (child.Evaluate())
            {
                case NodeState.Success:
                    return NodeState.Success;
                case NodeState.Failure:
                    continue;
                default:
                    break;
            }
        }
        return NodeState.Failure;
    }
}
