using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(NodeGenerator))]
public class Editor_NodeGenerator : Editor {
    bool toggle;
    string[] nodeStates;
    GUIStyle style;
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
        NodeGenerator nodeGenerator = (NodeGenerator)target;
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

        toggle = EditorGUILayout.BeginToggleGroup("Nodes list", toggle);

        if (toggle)
        {
            EditorGUILayout.BeginVertical();
            int i = 0;
            foreach (Node n in nodeGenerator.nodes)
            {
                EditorGUILayout.LabelField("Node[" +i.ToString()+ "] " + "Data", style, GUILayout.Height(50) , GUILayout.ExpandWidth(true));
                n.NodeState = (Node.NodeStates)EditorGUILayout.Popup("State", (int)n.NodeState, nodeStates);
                n.Position = EditorGUILayout.Vector2IntField("Position", n.Position);
                n.IsObstacle = EditorGUILayout.Toggle("Is Obstacle", n.IsObstacle);
                i++;
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndToggleGroup();

    }
}
