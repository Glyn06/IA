using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
        GameObject[] gos = GetAllObjectsInScene().ToArray();
        List<Collider2D> colliders = new List<Collider2D>();
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].GetComponent<Collider2D>() != null && gos[i].layer == LayerMask.NameToLayer("Static"))
            {
                colliders.Add(gos[i].GetComponent<Collider2D>());
            }
        }

        float radiusDistance = 1 / density;
        for (int i = 0; i < worldSize.x; i++)
        {
            for (int j = 0; j < worldSize.y; j++)
            {
                bool insideCollider = false;
                foreach (Collider2D col2D in colliders)
                {
                    if (col2D.OverlapPoint(new Vector2((float)i,(float)j)))
                    {
                        insideCollider = true;
                    }
                }
                if (!insideCollider)
                {
                    nodes.Add(new Node(new Vector2Int(i, j), Node.NodeStates.Close, false));
                }
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
