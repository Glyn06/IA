using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Node
{
    [SerializeField] private List<Node> adjacents;
    [SerializeField] private Vector2 position;
    [SerializeField] private NodeStates nodeState;
    [SerializeField] private NodeStates originalState;
    [SerializeField] private bool isObstacle;
    [SerializeField] private Node parentNode;
    [SerializeField] private bool used;

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

    public Node ParentNode
    {
        get { return parentNode; }
        set { parentNode = value; }
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
        nodeState = originalState = _state;
        used = false;
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

    public void OpenNode()
    {
        if (!IsObstacle && !used)
            nodeState = NodeStates.Open;
    }

    public void OpenNode(Node n)
    {
        if (!IsObstacle && !used)
        {
            parentNode = n;
            nodeState = NodeStates.Open;
        }
    }

    public void CloseNode() 
    {
        nodeState = NodeStates.Close;
        used = true;
    }

    public void RestartNode()
    {
        nodeState = originalState;
        used = false;
        parentNode = null;
    }
}
