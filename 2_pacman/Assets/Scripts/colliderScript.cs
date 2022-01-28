using UnityEngine;
using UnityEngine.Tilemaps;

public class colliderScript : MonoBehaviour
{
    private coinScript coinScript;
    private movementScript movementScript;
    [SerializeField] Grid grid;
    private ContactPoint2D[] collisionContactPoint = new ContactPoint2D[1];
    [SerializeField] Tilemap coinTilemap;
    private Vector3Int tileLocationToRemove;

    private void Awake()
    {
        coinScript = GetComponent<coinScript>();
        movementScript = GetComponent<movementScript>();
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.tag == "Player")
        {
            FindCoinTileToRemoveOnPlayerCollision(otherCollider);
            RemoveGivenTile();
            ReduceCoinAmount();
        }
    }

    private void FindCoinTileToRemoveOnPlayerCollision(Collision2D otherCollider)
    {
        otherCollider.GetContacts(collisionContactPoint);
        tileLocationToRemove = grid.WorldToCell(collisionContactPoint[0].point);
    }

    private void RemoveGivenTile()
    {
        coinTilemap.SetTile(tileLocationToRemove, null);
    }

    private void ReduceCoinAmount()
    {
        coinScript.coinAmount--;
    }
}