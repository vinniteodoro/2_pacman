using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class movementScript : MonoBehaviour
{
    public Rigidbody2D objectRigidBody { get; private set; }
    private float objectSpeed = 8f;
    private Vector2 objectInitialDirection;
    public LayerMask obstaclesLayer;
    public Vector2 objectDirection { get; private set; }
    public Vector2 objectNextDirectionBuffer { get; private set; }

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(objectNextDirectionBuffer != Vector2.zero)
        {
            SetObjectDirection(objectNextDirectionBuffer);
        }
    }

    private void FixedUpdate()
    {
        Vector2 objectCurrentPosition = objectRigidBody.position;
        Vector2 objectNextPosition = objectDirection * objectSpeed * Time.fixedDeltaTime;
        objectRigidBody.MovePosition(objectCurrentPosition + objectNextPosition);
    }

    public void SetObjectDirection(Vector2 objectWantedDirection)
    {
        if(!IsNextTileOccupied(objectWantedDirection))
        {
            objectDirection = objectWantedDirection;
            objectNextDirectionBuffer = Vector2.zero;
        }
        else
        {
            objectNextDirectionBuffer = objectDirection;
        }
    }

    public bool IsNextTileOccupied(Vector2 nextTile)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * .75f, 0.0f, objectDirection, 1f, obstaclesLayer);
        return hit.collider != null;
    }
}
