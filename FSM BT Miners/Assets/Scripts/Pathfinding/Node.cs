﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Node
{
    [SerializeField] private List<int> adjacentsIndex;
    [SerializeField] private Vector2 position;
    [SerializeField] private NodeStates nodeState;
    [SerializeField] private NodeStates originalState;
    [SerializeField] private bool isObstacle;
    [SerializeField] private Node parentNode;
    [SerializeField] private bool used;
    [SerializeField] private uint weight;
    [SerializeField] private uint totalWeight;


    public List<int> Adjacents
    {
        get {

            if (adjacentsIndex == null)
            {
                adjacentsIndex = new List<int>();
            }

            return adjacentsIndex; }
    }

    public uint TotalWeight
    {
        get { return totalWeight; }
        set { totalWeight = value; }
    }

    public uint Weight
    {
        get { return weight;  }
        set { weight = value; }
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

    public Node(Vector2 _position, NodeStates _state, bool _isObstacle, uint _weight)
    {
        isObstacle = _isObstacle;
        position = _position;
        nodeState = originalState = _state;
        used = false;
        weight = _weight;
    }

    public NodeStates GetState()
    {
        return nodeState;
    }

    public void AddConection(Node node, List<Node> list)
    {
        if (adjacentsIndex == null)
        {
            adjacentsIndex = new List<int>();
        }
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == node)
            {
                adjacentsIndex.Add(i);
                return;
            }
        }
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

            TotalWeight = parentNode.TotalWeight + Weight;
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
