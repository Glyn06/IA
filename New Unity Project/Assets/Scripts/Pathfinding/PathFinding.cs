using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    [System.Serializable]
    public enum PathType
    {
        breadthFirst = 0,
        depthFirst = 1,
        dijkstra = 2,
        aStar = 3,
    }

    private NodeManager nodeGenerator;
    private Node startNode;
    private Node destinationNode;
    private List<Node> openNodes;
    private List<Node> closedNodes;
    private uint totalWeight;

    public PathFinding()
    {
        openNodes = new List<Node>();
        closedNodes = new List<Node>();
    }

    public List<Vector2> GetPath (Vector2 startPosition, Vector2 destinationPosition, PathType pathType)
    {
        if (NodeManager.instance == null)
        {
            return new List<Vector2>();
        }
        nodeGenerator = NodeManager.instance;

        if (nodeGenerator.nodes == null)
        {
            return new List<Vector2>();
        }
        startNode = nodeGenerator.PositionToNode(startPosition);
        destinationNode = nodeGenerator.PositionToNode(destinationPosition);

        //Debug.Log("Start: " + startNode.Position);
        //Debug.Log("Destination: " + destinationNode.Position);

        startNode.OpenNode();
        openNodes.Add(startNode);
        while (openNodes.Count > 0)
        {
            Node n = GetOpenNode(pathType);
            if (n == destinationNode)
            {
                List<Node> nodePath = new List<Node>();
                nodePath.Add(n);
                nodePath = GeneratePath(nodePath, n);

                List<Vector2> path = new List<Vector2>();

                for (int i = 0; i < nodePath.Count; i++)
                {
                    path.Add(nodePath[i].Position);
                }

                //Un sabio de origen probablemente asiatico dejo una vez "En la programación no hay nada más definitivo que los fix temportales"
                //Ahora en serio, aveces el path da al revez beacouse he can
                if (path[0] == destinationNode.Position)
                {
                    path.Reverse();
                }

                ResetNodes();


                return path;
            }
            n.CloseNode();
            openNodes.Remove(n);
            closedNodes.Add(n);
            for (int i = 0; i < n.Adjacents.Count; i++)
            {
                if (nodeGenerator.nodes[n.Adjacents[i]].GetState() == Node.NodeStates.Ready)
                {
                    if (!nodeGenerator.nodes[n.Adjacents[i]].IsObstacle)
                    {
                        nodeGenerator.nodes[n.Adjacents[i]].OpenNode(n);

                        openNodes.Add(nodeGenerator.nodes[n.Adjacents[i]]);
                    }
                }
            }
        }
        ResetNodes();

        return new List<Vector2>();
    }

    private void ResetNodes()
    {
        for (int i = 0; i < openNodes.Count; i++)
        {
            openNodes[i].RestartNode();
        }

        openNodes.Clear();

        for (int i = 0; i < closedNodes.Count; i++)
        {
            closedNodes[i].RestartNode();
        }

        closedNodes.Clear();
    }
    private List<Node> GeneratePath(List<Node> list, Node n)
    {
        if (n.ParentNode != null)
        {
            list.Add(n.ParentNode);
            GeneratePath(list, n.ParentNode);
        }
        list.Reverse();
        return list;
    }

    private Node GetOpenNode(PathType type)
    {
        switch (type)
        {
            case PathType.breadthFirst:
                {
                    return openNodes[0];
                }
            case PathType.depthFirst:
                {
                    return openNodes[openNodes.Count - 1];
                }
            case PathType.dijkstra:
                {
                    Node n = null;
                    uint currentMinWeight = int.MaxValue;
                    for (int i = 0; i < openNodes.Count; i++)
                    {
                        if (openNodes[i].TotalWeight < currentMinWeight)
                        {
                            n = openNodes[i];
                            currentMinWeight = openNodes[i].TotalWeight;
                        }
                    }
                    return n;
                }
            case PathType.aStar:
                {
                    Node n = null;
                    uint currentMinWeightAndDistance = int.MaxValue;
                    for (int i = 0; i < openNodes.Count; i++)
                    {
                        if (openNodes[i].TotalWeight + ManhattanDistance(openNodes[i].Position, destinationNode.Position) < currentMinWeightAndDistance)
                        {
                            n = openNodes[i];
                            currentMinWeightAndDistance = openNodes[i].TotalWeight + ManhattanDistance(openNodes[i].Position, destinationNode.Position);
                        }
                    }
                    return n;
                }
        }
        return new Node(Vector2Int.zero,Node.NodeStates._count,false, 0);
    }

    private uint ManhattanDistance(Vector2 origin, Vector2 destination)
    {
        uint x = (uint)Mathf.Abs(origin.x - destination.x);
        uint y = (uint)Mathf.Abs(origin.y - destination.y);
        return x + y;
    }
}

