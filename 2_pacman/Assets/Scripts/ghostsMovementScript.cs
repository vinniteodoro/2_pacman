using UnityEngine;
using System.Collections.Generic;

public class ghostsMovementScript : MonoBehaviour
{
    private Rigidbody2D objectRigidBody;
    private float objectSpeed = 7f;
    private Vector2 objectDirection;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        RandomizeStartingGhostsDirection();
    }

    private void FixedUpdate()
    {
        MoveObject();
        VerifyMapBoundaries();
    }

    private void RandomizeStartingGhostsDirection()
    {
        var initialDirectionPossibilities = new List<Vector2>();
        initialDirectionPossibilities.Add(Vector2.left);
        initialDirectionPossibilities.Add(Vector2.right);
        var randomDirection = Random.Range(0, 2);
        SetObjectDirection(initialDirectionPossibilities[randomDirection]);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.tag == "Intersection")
        {
            
        }
    }

    private void VerifyMapBoundaries()
    {
        if(objectRigidBody.position.x >= movementScript.mapWidth) objectRigidBody.MovePosition(new Vector2(-movementScript.mapWidth + .1f, objectRigidBody.position.y));
        if(objectRigidBody.position.x <= -movementScript.mapWidth) objectRigidBody.MovePosition(new Vector2(movementScript.mapWidth - .1f, objectRigidBody.position.y));
    }

    private void MoveObject()
    {
        var objectCurrentPosition = objectRigidBody.position;
        var objectNextPosition = objectDirection * objectSpeed * Time.fixedDeltaTime;
        objectRigidBody.MovePosition(objectCurrentPosition + objectNextPosition);
    }

    public void SetObjectDirection(Vector2 objectWantedDirection)
    {
        objectDirection = objectWantedDirection;
    }
}