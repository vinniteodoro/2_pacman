using UnityEngine;

public class movementScript : MonoBehaviour
{
    private Rigidbody2D objectRigidBody;
    private float objectSpeed = 8f;
    private Vector2 objectDirection;
    private float mapWidth = 14.5f;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveObject();
        VerifyMapBoundaries();
    }   

    private void VerifyMapBoundaries()
    {
        if(objectRigidBody.position.x >= mapWidth) objectRigidBody.MovePosition(new Vector2(-mapWidth + .1f, objectRigidBody.position.y));
        if(objectRigidBody.position.x <= -mapWidth) objectRigidBody.MovePosition(new Vector2(mapWidth - .1f, objectRigidBody.position.y));
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