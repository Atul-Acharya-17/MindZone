using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    /// <summary>
    /// Delegate with a NodeState return type
    /// </summary>
    /// <returns></returns>
    public delegate NodeState Task();

    /// <summary>
    /// Action to be performed by the node
    /// </summary>
    Task action;

    /// <summary>
    /// Parameterized constructor
    /// </summary>
    /// <param name="task">task to be executed</param>
    public Leaf(Task task)
    {
        action = task;
    }

    /// <summary>
    /// Evaluates the node
    /// </summary>
    /// <returns>Returns the State of the Node</returns>
    public override NodeState Evaluate()
    {
        return action();
    }
}
