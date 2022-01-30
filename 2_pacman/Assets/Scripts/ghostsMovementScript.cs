using UnityEngine;
using System.Collections.Generic;

public class ghostsMovementScript : MonoBehaviour
{
    private Rigidbody2D objectRigidBody;
    private float objectSpeed = 7f;
    private Vector2 objectDirection;
    private Vector3Int tileToRaycastFrom, objectGridPosition;
    private ContactPoint2D[] colliderContactPoint = new ContactPoint2D[1];
    [SerializeField] Grid grid;
    [SerializeField] LayerMask obstaclesLayerMask;
    private RaycastHit2D upRaycast, downRaycast, leftRaycast, rightRaycast;
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
        DrawRaycasts();
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
            RandomizeWhichPathToGo();
            WhenToChangeDirection();
        }
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
        upRaycast = Physics2D.Raycast(rayCastOriginPosition, Vector2.up, 1, obstaclesLayerMask);
        downRaycast = Physics2D.Raycast(rayCastOriginPosition, Vector2.down, 1, obstaclesLayerMask);
        leftRaycast = Physics2D.Raycast(rayCastOriginPosition, Vector2.left, 1, obstaclesLayerMask);
        rightRaycast = Physics2D.Raycast(rayCastOriginPosition, Vector2.right, 1, obstaclesLayerMask);
    }

    private void DrawRaycasts()
    {
        if(upRaycast.collider == null) Debug.DrawRay(rayCastOriginPosition, Vector2.up, Color.green);
        else Debug.DrawRay(rayCastOriginPosition, Vector2.up, Color.red);
        
        if(downRaycast.collider == null) Debug.DrawRay(rayCastOriginPosition, Vector2.down, Color.green);
        else Debug.DrawRay(rayCastOriginPosition, Vector2.down, Color.red);

        if(leftRaycast.collider == null) Debug.DrawRay(rayCastOriginPosition, Vector2.left, Color.green);
        else Debug.DrawRay(rayCastOriginPosition, Vector2.left, Color.red);

        if(rightRaycast.collider == null) Debug.DrawRay(rayCastOriginPosition, Vector2.right, Color.green);
        else Debug.DrawRay(rayCastOriginPosition, Vector2.right, Color.red);
    }

    private void SaveRaycastsThatDidntHitAnything()
    {
        if(upRaycast.collider == null) ghostNextPossibleDirections.Add(Vector2.up);
        if(downRaycast.collider == null) ghostNextPossibleDirections.Add(Vector2.down);
        if(leftRaycast.collider == null) ghostNextPossibleDirections.Add(Vector2.left);
        if(rightRaycast.collider == null) ghostNextPossibleDirections.Add(Vector2.right);

        foreach(Vector2 direction in ghostNextPossibleDirections)
        {
            Debug.Log(direction);
        }
    }

    private void RandomizeWhichPathToGo()
    {
        
    }

    private void WhenToChangeDirection()
    {

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