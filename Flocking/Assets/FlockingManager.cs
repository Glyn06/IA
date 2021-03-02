using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlockingManager : MonoBehaviour
{
    private List<Boid> boids = new List<Boid>();

    public Vector2 center = Vector2.zero;
    public float radius = 15f;

    public Boid boidPrefab;
    [Range(10, 500)]
    public int boidCount = 250;
    const float boidDensity = 0.08f;

    [Range (1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float separationRadiusMultiplier = 0.5f;
    [Range(1f, 5f)]
    public float boidSpeed = 2.5f;

    [Range(0f, 4f)]
    public float aligmentWeigth = 2.5f;
    [Range(0f, 4f)]
    public float cohesionWeigth = 1f;
    [Range(0f, 4f)]
    public float separationWeigth = 4f;
    [Range(0f, 4f)]
    public float radiusWeigth = 0.01f;


    float sqrNeighbourRadius;
    float sqrSeparationRadious;
    public float SquareSeparationRadius { get { return sqrSeparationRadious; } }

    private void Start()
    {
        for (int i = 0; i < boidCount; i++)
        {
            Boid newBoid = Instantiate(boidPrefab,
                                       Random.insideUnitCircle * boidCount * boidDensity,
                                       Quaternion.Euler(Vector3.forward * Random.Range(0f,360f)),
                                       transform);
            newBoid.name = "Boid " + i;

            boids.Add(newBoid);
        }

        sqrNeighbourRadius = neighbourRadius * neighbourRadius;
        sqrSeparationRadious = sqrNeighbourRadius * separationRadiusMultiplier * separationRadiusMultiplier;
    }

    private void Update()
    {
        foreach (Boid b in boids)
        {
            List<Transform> context = GetNearbyObjects(b);
            //b.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector2 move = calculateMove(b, context);

            b.Move(move * boidSpeed);
        }
    }

    private List<Transform> GetNearbyObjects(Boid b)
    {
        List<Transform> nearbyObjects = new List<Transform>();
        Collider2D[] nearbyObjectsColliders = Physics2D.OverlapCircleAll(b.transform.position, neighbourRadius);
        foreach (Collider2D c in nearbyObjectsColliders)
        {
            if (c != b.BoidCollider)
                nearbyObjects.Add(c.transform);
        }

        return nearbyObjects;
    }

    public Vector2 calculateMove(Boid boid, List<Transform> context)
    {
        //Aligment
        Vector2 aligmentMove = Vector2.zero;
        aligmentMove = CalculateAlignment(boid, context) * aligmentWeigth;

        //Cohesion
        Vector2 cohesionMove = Vector2.zero;
        cohesionMove = CalculateCohesion(boid, context) * cohesionWeigth;

        //Separation
        Vector2 separationMove = Vector2.zero;
        separationMove = CalculateSeparation(boid, context) * separationWeigth;

        //StayInCenter
        Vector2 radiusMove = Vector2.zero;
        radiusMove = StayInRadius(boid) * radiusWeigth;

        Vector2 move = aligmentMove + cohesionMove + separationMove + radiusMove;
        move.Normalize();

        return move;
    }

    public Vector2 CalculateAlignment(Boid boid, List<Transform> context)
    {
        if (context.Count == 0)
            return boid.transform.up;

        Vector2 aligmentMove = Vector2.zero;
        foreach (Transform item in context)
        {
            aligmentMove += (Vector2)item.transform.up;
        }

        aligmentMove /= context.Count;

        return aligmentMove;
    }

    public Vector2 CalculateCohesion(Boid boid, List<Transform> context)
    {
        if (context.Count == 0)
            return Vector2.zero;

        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }

        cohesionMove /= context.Count;

        cohesionMove -= (Vector2)boid.transform.position;

        return cohesionMove;
    }

    public Vector2 CalculateSeparation(Boid boid, List<Transform> context)
    {
        if (context.Count == 0)
            return Vector2.zero;

        Vector2 separationMove = Vector2.zero;
        int nAvoid = 0;

        foreach (Transform item in context)
        {
            if (Vector2.SqrMagnitude(item.position - boid.transform.position) < sqrSeparationRadious)
            {
                nAvoid++;
                separationMove += (Vector2)(boid.transform.position - item.position);
            }
        }

        if (nAvoid > 0)
            separationMove /= nAvoid;

        return separationMove;
    }

    public Vector2 StayInRadius(Boid boid) {
        Vector2 centerOffset = center - (Vector2)boid.transform.position;
        float t = centerOffset.magnitude / radius;
        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }


}