using UnityEngine;

public class movementScript : MonoBehaviour
{
    private Rigidbody2D objectRigidBody;
    private float objectSpeed = 8f;
    private Vector2 objectDirection;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveObject();
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