using UnityEngine;

public class ghostColliderScript : MonoBehaviour
{
    private movementScript movementScript;
    private gameManagerScript gMScript;
    private ghostsMovementScript ghostsMovementScript;

    private void Awake()
    {
        gMScript = GetComponent<gameManagerScript>();
        movementScript = GetComponent<movementScript>();
        ghostsMovementScript = GetComponent<ghostsMovementScript>();
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.tag == "Player")
        {
            var playerAteGhost = true;
            if(playerAteGhost) ResetGhost();
            else PlayerLost();
        }
    }

    private void ResetGhost()
    {
        ghostsMovementScript.RandomizeStartingGhostsDirection();
    }

    private void PlayerLost()
    {

    }
}
