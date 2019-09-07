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
    private bool selectionFoldOut = false;
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
        NodeManager nodeManager = (NodeManager)target;
        style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter , fontSize = 25, };

        if (GUILayout.Button("Generate Nodes"))
        {
            nodeManager.ClearNodes();
            nodeManager.GenerateNodes();
        }

        if (GUILayout.Button("Clear Nodes"))
        {
            nodeManager.ClearNodes();
        }

        if (GUILayout.Button("Reset Weight"))
        {
            nodeManager.ResetNodesWeight();
        }

        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        
        selectionFoldOut = EditorGUILayout.Foldout(selectionFoldOut, "Weight Selection");
        if (selectionFoldOut)
        {
            nodeManager.weightSelection.selectionColor = EditorGUILayout.ColorField("Selection Color",nodeManager.weightSelection.selectionColor, null);
            nodeManager.weightSelection.selection = EditorGUILayout.RectField("Selection", nodeManager.weightSelection.selection, null);
            nodeManager.weightSelection.weight = (uint)EditorGUILayout.IntSlider("Weight", (int)nodeManager.weightSelection.weight, 1, 10);
            if (GUILayout.Button("Set Weight"))
            {
                nodeManager.SetNodesWeight();
            }

        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(this.target);
        }

       

        EditorGUILayout.Space();
        if (nodeManager.nodes != null)
        {
            EditorGUILayout.LabelField("Nodes Count = " + nodeManager.nodes.Count.ToString());


            nodeManager.drawGizmos = EditorGUILayout.Toggle("Draw Gizmos", nodeManager.drawGizmos);
            if (nodeManager.drawGizmos)
            {
                EditorGUILayout.BeginHorizontal();

                nodeManager.drawIndex = EditorGUILayout.Toggle("Draw Index", nodeManager.drawIndex);
                if (nodeManager.drawIndex)
                {
                    nodeManager.drawWeight = false;
                }
                nodeManager.drawWeight = EditorGUILayout.Toggle("Draw Weight", nodeManager.drawWeight);
                if (nodeManager.drawWeight)
                {
                    nodeManager.drawIndex = false;
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                nodeManager.drawIndex = false;
                nodeManager.drawWeight = false;
            }

            from = EditorGUILayout.IntField("Show From", from);
            to = EditorGUILayout.IntField("To", to);
            if (from < 0)
                from = 0;
            if (to < from)
                to = from;

            toggle = EditorGUILayout.Foldout(toggle, "Nodes list");

            if (toggle)
            {
                EditorGUILayout.BeginVertical();
                int i = 0;
                foreach (Node n in nodeManager.nodes)
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
        }
    }
}
