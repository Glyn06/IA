using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour {

    public Vector2Int worldSize;
    [Range(0.01f, 10.0f)] public float density;
    [HideInInspector] public List<Node> nodes;

    void Start () {
    }

    void Update () {

	}

    public void GenerateNodes()
    {
        nodes = new List<Node>();
        float radiusDistance = 1 / density;
        for (int i = 0; i < worldSize.x; i++)
        {
            for (int j = 0; j < worldSize.y; j++)
            {
                nodes.Add(new Node(new Vector2Int(i, j), Node.NodeStates.Close, false));
            }
        }


    }

    public void ClearNodes()
    {
        nodes.Clear();
    }
}
