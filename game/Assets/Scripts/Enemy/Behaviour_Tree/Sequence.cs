using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    /// <summary>
    /// Children nodes of the sequence node
    /// </summary>
    private Node[] children;

    /// <summary>
    /// Parameterized constuctor
    /// </summary>
    /// <param name="nodes">List of children nodes</param>
    public Sequence(params Node[] nodes)
    {
        children = nodes;
    }

    /// <summary>
    /// Executes the Sequence node functionality
    /// </summary>
    /// <returns>State of the sequence node</returns>
    public override NodeState Evaluate()
    { 
        foreach (Node child in children)
        {
            switch(child.Evaluate())
            {
                case NodeState.Success:
                    continue;
                case NodeState.Failure:
                    return NodeState.Failure;
                default:
                    break;
            }
        }
        return NodeState.Success;
    }
}
