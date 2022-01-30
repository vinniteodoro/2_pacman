using UnityEngine;
using System.Collections.Generic;

public class ghostsMovementScript : MonoBehaviour
{
    private Rigidbody2D objectRigidBody;
    private float objectSpeed = 7f;
    private Vector2 objectDirection;
    private List<Vector2> directionChoices = new List<Vector2>();

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody2D>();
        directionChoices.Add(Vector2.left);
        directionChoices.Add(Vector2.right);
        directionChoices.Add(Vector2.up);
        directionChoices.Add(Vector2.down);
    }

    private void Start()
    {
        RandomizeStartingGhostsDirection();
    }

    private void FixedUpdate()
    {
        MoveObject();
        VerifyMapBoundaries();
        //RandomizeGhostsDirectionIfStopped();
    }

    private void RandomizeStartingGhostsDirection()
    {
        var randomDirection = Random.Range(0, 2);
        SetObjectDirection(directionChoices[randomDirection]);
    }

    private void RandomizeGhostsDirectionIfStopped()
    {   /*
        if(ghosts are stopped)
        {
            var randomDirection = Random.Range(0, 4);
            SetObjectDirection(directionChoices[randomDirection]);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.tag == "Intersection")
        {
            RandomizeGhostsDirection();
        }
    }

    private void RandomizeGhostsDirection()
    {
        var randomDirection = Random.Range(0, 4);
        SetObjectDirection(directionChoices[randomDirection]);
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