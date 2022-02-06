using UnityEngine;

public class ghostColliderScript : MonoBehaviour
{
    private movementScript movementScript;
    private ghostsMovementScript ghostsMovementScript;
    [SerializeField] GameObject gameManagerObject;

    private void Awake()
    {
        movementScript = GetComponent<movementScript>();
        ghostsMovementScript = GetComponent<ghostsMovementScript>();
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.tag == "Player")
        {
            var playerAteGhost = false;
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
        gameManagerObject.GetComponent<gameManagerScript>().GameOver();
    }
}
