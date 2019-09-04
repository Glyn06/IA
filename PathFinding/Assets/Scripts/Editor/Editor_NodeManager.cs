using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(NodeManager))]
public class Editor_NodeManager : Editor {
    private bool toggle;
    private int from;
    private int to;
    private string[] nodeStates;
    private GUIStyle style;



    private void OnEnable()
    {
        nodeStates = new string[(int)Node.NodeStates._count];
        for(Node.NodeStates n = Node.NodeStates.Open; n < Node.NodeStates._count; n++)
        {
            nodeStates[(int)n] = n.ToString();
        }

    }
    public override void OnInspectorGUI()
    {
        NodeManager nodeGenerator = (NodeManager)target;
        style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter , fontSize = 25, };

        if (GUILayout.Button("Generate Nodes"))
        {
            nodeGenerator.ClearNodes();
            nodeGenerator.GenerateNodes();
        }

        if (GUILayout.Button("Clear Nodes"))
        {
            nodeGenerator.ClearNodes();
        }

        base.OnInspectorGUI();
        EditorGUILayout.Space();
        if (nodeGenerator.nodes != null)
        {
            EditorGUILayout.LabelField("Nodes Count = " + nodeGenerator.nodes.Count.ToString());


            nodeGenerator.drawGizmos = EditorGUILayout.Toggle("Draw Gizmos", nodeGenerator.drawGizmos);
            if (nodeGenerator.drawGizmos)
            {
                EditorGUILayout.BeginHorizontal();

                nodeGenerator.drawIndex = EditorGUILayout.Toggle("Draw Index", nodeGenerator.drawIndex);
                if (nodeGenerator.drawIndex)
                {
                    nodeGenerator.drawWeight = false;
                }
                nodeGenerator.drawWeight = EditorGUILayout.Toggle("Draw Weight", nodeGenerator.drawWeight);
                if (nodeGenerator.drawWeight)
                {
                    nodeGenerator.drawIndex = false;
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                nodeGenerator.drawIndex = false;
                nodeGenerator.drawWeight = false;
            }

            from = EditorGUILayout.IntField("Show From", from);
            to = EditorGUILayout.IntField("To", to);
            if (from < 0)
                from = 0;
            if (to < from)
                to = from;

            toggle = EditorGUILayout.BeginToggleGroup("Nodes list", toggle);

            if (toggle)
            {
                EditorGUILayout.BeginVertical();
                int i = 0;
                foreach (Node n in nodeGenerator.nodes)
                {
                    if (i >= from && i <= to)
                    {
                        EditorGUILayout.LabelField("Node[" + i.ToString() + "] " + "Data", style, GUILayout.Height(50), GUILayout.ExpandWidth(true));
                        EditorGUILayout.Popup("State", (int)n.NodeState, nodeStates);
                        EditorGUILayout.Vector2Field("Position", n.Position);
                        EditorGUILayout.Toggle("Is Obstacle", n.IsObstacle);
                        EditorGUILayout.LabelField("Conections: " + n.Adjacents.Count.ToString());
                    }
                    i++;
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndToggleGroup();
        }
    }
}
