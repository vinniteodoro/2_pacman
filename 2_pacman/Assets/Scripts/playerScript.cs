using UnityEngine;

public class playerScript : MonoBehaviour
{
    private movementScript movementScript;

    private void Awake()
    {
        movementScript = GetComponent<movementScript>();
    }

    private void Update()
    {
        SetPlayerDirectionToMove();
        SetPlayerSpriteOrientation();
    }

    private void SetPlayerSpriteOrientation()
    {
        var playerAngle = Mathf.Atan2(movementScript.objectDirection.y, movementScript.objectDirection.x);
        transform.rotation = Quaternion.AngleAxis(playerAngle * Mathf.Rad2Deg, Vector3.forward);
    }

    private void SetPlayerDirectionToMove()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) movementScript.SetObjectDirection(Vector2.up);
        if(Input.GetKeyDown(KeyCode.DownArrow)) movementScript.SetObjectDirection(Vector2.down);
        if(Input.GetKeyDown(KeyCode.LeftArrow)) movementScript.SetObjectDirection(Vector2.left);
        if(Input.GetKeyDown(KeyCode.RightArrow)) movementScript.SetObjectDirection(Vector2.right);
    }
}