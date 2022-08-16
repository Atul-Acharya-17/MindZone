using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : Node
{
    /// <summary>
    /// Child of the root node
    /// </summary>
    Node child;

    /// <summary>
    /// Parameterized constructor
    /// </summary>
    /// <param name="node">Child of the root node</param>
    public BehaviourTree(Node node)
    {
        child = node;
    }

    /// <summary>
    /// Starts the execution of the Behaviour Tree
    /// </summary>
    /// <returns>Returns the State of the Node</returns>
    public override NodeState Evaluate()
    {
        return child.Evaluate();
    }
}
