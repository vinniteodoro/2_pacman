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
        if(Input.GetKeyDown(KeyCode.UpArrow)) movementScript.SetObjectDirection(Vector2.up);
        if(Input.GetKeyDown(KeyCode.DownArrow)) movementScript.SetObjectDirection(Vector2.down);
        if(Input.GetKeyDown(KeyCode.LeftArrow)) movementScript.SetObjectDirection(Vector2.left);
        if(Input.GetKeyDown(KeyCode.RightArrow)) movementScript.SetObjectDirection(Vector2.right);
    }
}