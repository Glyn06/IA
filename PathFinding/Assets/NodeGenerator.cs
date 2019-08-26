using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour {

    public Vector2Int worldSize;
    [Range(0.01f, 10.0f)] public float density;
    [SerializeField] private bool drawGizmos;
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

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            foreach (Node node in nodes)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(new Vector3((float)node.Position.x,(float)node.Position.y, 0.0f), 0.1f);
            }
        }

    }

    public void ClearNodes()
    {
        nodes.Clear();
    }
}
