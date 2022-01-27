using UnityEngine;
using UnityEngine.Tilemaps;

public class colliderScript : MonoBehaviour
{
    private coinScript coinScript;
    private gameManagerScript gameManagerScript;

    private void Awake()
    {
        coinScript = GetComponent<coinScript>();
        gameManagerScript = GetComponent<gameManagerScript>();
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.tag == "Coin")
        {
            var coinCollidedContactPoints = otherCollider.contacts[0].point;
            var coinTilemap = otherCollider.gameObject.GetComponent<Tilemap>();
            var coinToRemove = coinTilemap.WorldToCell(coinCollidedContactPoints);
            coinTilemap.SetTile(coinToRemove, null);  
        }

        if(otherCollider.gameObject.tag == "Player")
        {
            coinScript.PlayerHitCoin();
        }
    }
}