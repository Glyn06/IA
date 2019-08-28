using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    [SerializeField] private List<Node> adjacents;
    [SerializeField] private Vector2 position;
    [SerializeField] private NodeStates nodeState;
    [SerializeField] private bool isObstacle;
    
    public List<Node> Adjacents
    {
        get {

            if (adjacents == null)
            {
                adjacents = new List<Node>();
            }

            return adjacents; }
    }

    public NodeStates NodeState
    {
        get { return nodeState;  }
        set { nodeState = value; }
    }

    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public bool IsObstacle
    {
        get { return isObstacle; }
        set { isObstacle = value; }
    }

    public enum NodeStates
    {
        Open,
        Close,
        Ready,
        [HideInInspector]_count
    }

    public Node(Vector2 _position, NodeStates _state, bool _isObstacle)
    {
        isObstacle = _isObstacle;
        position = _position;
        nodeState = _state;
    }

    public NodeStates GetState()
    {
        return nodeState;
    }

    public void AddConection(Node node)
    {
        if (adjacents == null)
        {
            adjacents = new List<Node>();
        }
        adjacents.Add(node);
    }
}
