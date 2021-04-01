using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    /// <summary>
    /// Status of the Node
    /// </summary>
    /// <remarks>
    /// Success: Node has achieved its goal
    /// Failure: Node had failed to achieve its goal
    /// </remarks>
    public enum NodeState
    {
        Success,
        Failure
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public Node()
    {

    }

    /// <summary>
    /// Abstract method to be implemented by deriving classes
    /// </summary>
    /// <returns>Returns the State of the Node</returns>
    public abstract NodeState Evaluate();
}