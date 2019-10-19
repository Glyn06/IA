using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public static FlockingManager instance;
    public GameObject flockPoint;
    private Boid[] boids;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        boids = FindObjectsOfType<Boid>();
    }
    public Vector2 Alignment(Boid boid)
    {
        List<Boid> insideRadiusBoids = GetInsideRadiusBoids(boid);
        Vector2 avg = Vector2.zero;
        foreach (Boid b in insideRadiusBoids)
            avg += (Vector2)b.transform.up.normalized;
        avg /= insideRadiusBoids.Count;
        avg.Normalize();
        return avg;
    }

    public Vector2 Cohesion(Boid boid)
    {
        List<Boid> insideRadiusBoids = GetInsideRadiusBoids(boid);
        Vector2 avg = Vector2.zero;
        foreach (Boid b in insideRadiusBoids)
            avg += b.currentPosition;
        avg /= insideRadiusBoids.Count;
        return (avg - boid.currentPosition).normalized;
    }

    public Vector2 Separation(Boid boid)
    {
        List<Boid> insideRadiusBoids = GetInsideRadiusBoids(boid);
        Vector2 avg = Vector2.zero;
        foreach (Boid b in insideRadiusBoids)
            avg += (b.currentPosition - boid.currentPosition);
        avg /= insideRadiusBoids.Count;
        avg *= -1;
        avg.Normalize();
        return avg;
    }

    public Vector2 Direction(Boid boid, GameObject target) {
      
        return ((Vector2)target.transform.position - boid.currentPosition).normalized;
    }

    public List<Boid> GetInsideRadiusBoids(Boid boid)
    {
        List<Boid> insideRadiusBoids = new List<Boid>();
        foreach (Boid b in boids)
            if (boid.circleCollider2D.OverlapPoint(b.currentPosition))
                insideRadiusBoids.Add(b);
        return insideRadiusBoids;
    }
}