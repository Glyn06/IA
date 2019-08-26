using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class NodeGenerator : MonoBehaviour
{

    public Vector2Int worldSize;
    [Range(1, 4)] public int density;
    [SerializeField] private bool drawGizmos;
    [HideInInspector] public List<Node> nodes;

    public void GenerateNodes()
    {
        nodes = new List<Node>();
        GameObject[] gos = GetAllObjectsInScene().ToArray();
        List<Collider2D> colliders = new List<Collider2D>();

        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].GetComponent<Collider2D>() != null && gos[i].layer == LayerMask.NameToLayer("Static"))
            {
                colliders.Add(gos[i].GetComponent<Collider2D>());
            }
        }

        for (int i = 0; i < worldSize.x * density; i++)
        {
            for (int j = 0; j < worldSize.y * density; j++)
            {
                bool insideCollider = false;
                foreach (Collider2D col2D in colliders)
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

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (drawGizmos)
        {
            foreach (Node node in nodes)
            {
                Gizmos.DrawSphere(new Vector3((float)node.Position.x,(float)node.Position.y, 0.0f), 0.1f);
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
        nodes.Clear();
    }
}
