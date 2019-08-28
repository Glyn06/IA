using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class NodeGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct ConectionColors
    {
        public Color obstacleNodeColor;
        public Color zeroConectionNodeColor;
        public Color oneConectionNodeColor;
        public Color twoConectionNodeColor;
        public Color threeConectionNodeColor;
        public Color fourConectionNodeColor;
    }

    public Vector2Int worldSize;
    [Range(1, 4)] public int density;
    [SerializeField] private bool drawGizmos;
    [HideInInspector] public List<Node> nodes;
    [SerializeField] [HideInInspector] private List<Collider2D> obstacleColliders;
    public ConectionColors nodeColors;

    private const string staticLayer = "Static";
    private const string obstacleLayer = "Obstacle";


    private void Update()
    {
        if (!drawGizmos)
        {
            foreach (Node node in nodes)
            {
                node.IsObstacle = false;
                foreach (Collider2D col2D in obstacleColliders)
                {
                    if (col2D.OverlapPoint(node.Position))
                    {
                        node.IsObstacle = true;
                    }
                }
            }
        }
    }
    public void GenerateNodes()
    {
        nodes = new List<Node>();
        GameObject[] gos = GetAllObjectsInScene().ToArray();
        List<Collider2D> staticColliders = new List<Collider2D>();
        obstacleColliders = new List<Collider2D>();

        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].GetComponent<Collider2D>() != null)
            {
                if (gos[i].layer == LayerMask.NameToLayer(staticLayer))
                {
                    staticColliders.Add(gos[i].GetComponent<Collider2D>());
                }
                if (gos[i].layer == LayerMask.NameToLayer(obstacleLayer))
                {
                    obstacleColliders.Add(gos[i].GetComponent<Collider2D>());
                }
            }
        }

        for (int i = 0; i < worldSize.x * density; i++)
        {
            for (int j = 0; j < worldSize.y * density; j++)
            {
                bool insideCollider = false;
                foreach (Collider2D col2D in staticColliders)
                {
                    if (col2D.OverlapPoint(new Vector2(((float)i / (float)density), ((float)j / (float)density))))
                    {
                        insideCollider = true;
                    }
                }
                if (!insideCollider)
                {
                    nodes.Add(new Node(new Vector2(((float)i / (float)density), ((float)j / (float)density)), Node.NodeStates.Close, false));
                }
            }
        }

        foreach (Node node in nodes)
        {
            foreach (Collider2D col2D in obstacleColliders)
            {
                if (col2D.OverlapPoint(node.Position))
                {
                    node.IsObstacle = true;
                }
            }
        }

        Vector2 rightDistance = new Vector2(((float)(1.0f / (float)density)), 0.0f);
        Vector2 upDistance = new Vector2(0.0f,((float)(1.0f / (float)density)));


        RaycastHit2D raycastHit2D;

        foreach (Node currentNode in nodes)
        {
            foreach (Node node in nodes)
            {
                if (currentNode.Position + upDistance == node.Position)
                {
                    raycastHit2D = Physics2D.Raycast(currentNode.Position, Vector2.up, upDistance.y);
                    if (raycastHit2D.collider == null || raycastHit2D.collider.gameObject.layer == LayerMask.NameToLayer(obstacleLayer))
                    {
                        node.AddConection(currentNode);
                        currentNode.AddConection(node);
                    }
                
                }

                if (currentNode.Position + rightDistance == node.Position)
                {
                    raycastHit2D = Physics2D.Raycast(currentNode.Position, Vector2.right, rightDistance.x);
                    if (raycastHit2D.collider == null || raycastHit2D.collider.gameObject.layer == LayerMask.NameToLayer(obstacleLayer))
                    {
                        node.AddConection(currentNode);
                        currentNode.AddConection(node);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (nodes != null)
        {
            foreach (Node node in nodes)
            {
                node.IsObstacle = false;
                foreach (Collider2D col2D in obstacleColliders)
                {
                    if (col2D.OverlapPoint(node.Position))
                    {
                        node.IsObstacle = true;
                    }
                }
            }

            if (drawGizmos)
            {
                foreach (Node node in nodes)
                {
                    if (node.IsObstacle)
                    {
                        Gizmos.color = nodeColors.obstacleNodeColor;
                    }
                    else
                    {
                        switch(node.Adjacents.Count)
                        {
                            case 0:
                                Gizmos.color = nodeColors.zeroConectionNodeColor;
                                break;
                            case 1:
                                Gizmos.color = nodeColors.oneConectionNodeColor;
                                break;
                            case 2:
                                Gizmos.color = nodeColors.twoConectionNodeColor;
                                break;
                            case 3:
                                Gizmos.color = nodeColors.threeConectionNodeColor;
                                break;
                            case 4:
                                Gizmos.color = nodeColors.fourConectionNodeColor;
                                break;
                        }

                    }
                    Gizmos.DrawWireSphere(new Vector3((float)node.Position.x, (float)node.Position.y, 0.0f), 0.1f);
                }
            }
        }


    }

    private List<GameObject> GetAllObjectsInScene()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.hideFlags != HideFlags.None)
                continue;

            if (PrefabUtility.GetPrefabType(go) == PrefabType.Prefab || PrefabUtility.GetPrefabType(go) == PrefabType.ModelPrefab)
                continue;

            objectsInScene.Add(go);
        }
        return objectsInScene;
    }

    public void ClearNodes()
    {
        if (nodes != null)
        {
            nodes.Clear();
        }
    }
}
