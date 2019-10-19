using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    FlockingManager fM;
    public float speed = 2.5f;
    public float turnSpeed = 5f;
    public Vector2 currentPosition;
    public Vector3 currentRotation;
    public CircleCollider2D circleCollider2D;

    private void Start()
    {
        fM = FlockingManager.instance;
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        currentPosition = transform.position;
        transform.up = Vector3.Lerp(transform.up, ACS(),turnSpeed* Time.deltaTime);
    }

    public Vector2 ACS() {

        Vector2 ACS = fM.Alignment(this) + fM.Cohesion(this) + fM.Separation(this) + fM.Direction(this,fM.flockPoint);
        ACS.Normalize();

        return ACS;
    }
}
