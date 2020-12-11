using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{

    public GameObject target;
    private PathFinding pathFinding;
    public List<Vector2> path;
    private int currentPosition = 0;

    public float delay = 2f;


    public PathFinding.PathType pathType;

    // Use this for initialization
    void Start()
    {
        path = new List<Vector2>();
        pathFinding = new PathFinding();

        //path = pathFinding.GetPath(transform.position, (Vector2)target.transform.position, pathType);
        /*Debug.Log(path.Count);
        Invoke("Move", 0.1f);*/
    }

    public void FindPath(GameObject _startGO, GameObject _endGO)
    {
        currentPosition = 0;
        target = _endGO;
        path = pathFinding.GetPath(_startGO.transform.position, target.transform.position, pathType);
    }

    public bool Move(float timer)
    {
        if (timer >= delay && currentPosition < path.Count)
        {
            transform.position = path[currentPosition];
            currentPosition++;

            return true;
        }

        return false;
    }
}
