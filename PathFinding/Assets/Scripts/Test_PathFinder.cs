using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PathFinder : MonoBehaviour {

    public GameObject target;
    private PathFinding pathFinding;
    public List<Vector2> path;
    private int currentPosition = 0;
	// Use this for initialization
	void Start () {
        path = new List<Vector2>();
        pathFinding = new PathFinding();
        path = pathFinding.GetPath(transform.position, (Vector2)target.transform.position);
        Invoke("Move", 0.1f);
    }
    public void Move()
    {
        if (currentPosition < path.Count)
        {
            transform.position = path[currentPosition];
            currentPosition++;
            Invoke("Move", 0.1f);
        }

    }
}
