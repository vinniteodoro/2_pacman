using UnityEngine;
using System.Collections.Generic;

public class ghostsMovementScript : MonoBehaviour
{
    private Rigidbody2D objectRigidBody;
    private float objectSpeed = 7f;
    private Vector2 objectDirection, nextChosenDirection;
    private Vector3Int tileToRaycastFrom, objectGridPosition;
    private ContactPoint2D[] colliderContactPoint = new ContactPoint2D[1];
    [SerializeField] Grid grid;
    [SerializeField] LayerMask obstaclesLayerMask;
    private RaycastHit2D ghostRaycast, upIntersectionRaycast, downIntersectionRaycast, leftIntersectionRaycast, rightIntersectionRaycast;
    private Vector2 rayCastOriginPosition;
    private List<Vector2> ghostNextPossibleDirections = new List<Vector2>();

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
        //DrawRaycasts();
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
            ClearPreviousPossibleDirectionsList();
            PrepareTileToRaycastFrom();
            GenerateRaycastsInAllDirections();
            DrawRaycasts();
            SaveRaycastsThatDidntHitAnything();
            RandomizeWhichPathToGoFromPossibleOptions();
            
        }
    }

    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        ChangeDirectionWhenAvailable();
    }

    private void ClearPreviousPossibleDirectionsList()
    {
        ghostNextPossibleDirections.Clear();
    }

    private void PrepareTileToRaycastFrom()
    {
        objectGridPosition = grid.WorldToCell(objectRigidBody.position);

        if(objectDirection.x == -1) tileToRaycastFrom = objectGridPosition + new Vector3Int(-1, 0, 0);
        if(objectDirection.x == 1) tileToRaycastFrom = objectGridPosition + new Vector3Int(1, 0, 0);
        if(objectDirection.y == -1) tileToRaycastFrom = objectGridPosition + new Vector3Int(0, -1, 0);
        if(objectDirection.y == 1) tileToRaycastFrom = objectGridPosition + new Vector3Int(0, 1, 0);

        rayCastOriginPosition = new Vector2(tileToRaycastFrom.x + .5f, tileToRaycastFrom.y+.5f);
    }

    private void GenerateRaycastsInAllDirections()
    {
        upIntersectionRaycast = Physics2D.Raycast(rayCastOriginPosition, Vector2.up, 1, obstaclesLayerMask);
        downIntersectionRaycast = Physics2D.Raycast(rayCastOriginPosition, Vector2.down, 1, obstaclesLayerMask);
        leftIntersectionRaycast = Physics2D.Raycast(rayCastOriginPosition, Vector2.left, 1, obstaclesLayerMask);
        rightIntersectionRaycast = Physics2D.Raycast(rayCastOriginPosition, Vector2.right, 1, obstaclesLayerMask);
    }

    private void DrawRaycasts()
    {
        if(upIntersectionRaycast.collider == null) Debug.DrawRay(rayCastOriginPosition, Vector2.up, Color.green);
        else Debug.DrawRay(rayCastOriginPosition, Vector2.up, Color.red);
        
        if(downIntersectionRaycast.collider == null) Debug.DrawRay(rayCastOriginPosition, Vector2.down, Color.green);
        else Debug.DrawRay(rayCastOriginPosition, Vector2.down, Color.red);

        if(leftIntersectionRaycast.collider == null) Debug.DrawRay(rayCastOriginPosition, Vector2.left, Color.green);
        else Debug.DrawRay(rayCastOriginPosition, Vector2.left, Color.red);

        if(rightIntersectionRaycast.collider == null) Debug.DrawRay(rayCastOriginPosition, Vector2.right, Color.green);
        else Debug.DrawRay(rayCastOriginPosition, Vector2.right, Color.red);
    }

    private void SaveRaycastsThatDidntHitAnything()
    {
        if(upIntersectionRaycast.collider == null) ghostNextPossibleDirections.Add(Vector2.up);
        if(downIntersectionRaycast.collider == null) ghostNextPossibleDirections.Add(Vector2.down);
        if(leftIntersectionRaycast.collider == null) ghostNextPossibleDirections.Add(Vector2.left);
        if(rightIntersectionRaycast.collider == null) ghostNextPossibleDirections.Add(Vector2.right);
    }

    private void RandomizeWhichPathToGoFromPossibleOptions()
    {
        nextChosenDirection = ghostNextPossibleDirections[Random.Range(0, ghostNextPossibleDirections.Count)];
    }

    private void ChangeDirectionWhenAvailable()
    {
        ghostRaycast = Physics2D.BoxCast(objectRigidBody.position, new Vector2(.477f, .477f), 0, nextChosenDirection, 1, obstaclesLayerMask);
        if(ghostRaycast.collider == null) 
        {
            Debug.DrawRay(objectRigidBody.position, nextChosenDirection, Color.green); 
            SetObjectDirection(nextChosenDirection);
        }
        else Debug.DrawRay(objectRigidBody.position, nextChosenDirection, Color.red);
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