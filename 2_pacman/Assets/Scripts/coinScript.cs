using UnityEngine;
using UnityEngine.Tilemaps;

public class coinScript : MonoBehaviour
{
    [SerializeField] Tilemap coinTilemap;
    [SerializeField] Sprite coinSprite;
    public static int coinAmount = 1;
    private gameManagerScript gameManagerScript;

    private void Awake()
    {
        coinTilemap = this.gameObject.GetComponent<Tilemap>();
        gameManagerScript = GetComponent<gameManagerScript>();
        CheckStartingGameCoinAmount();
    }

    private void CheckStartingGameCoinAmount()
    {
        var tilemapBounds = coinTilemap.cellBounds;
        
        foreach(Vector3Int tilemapPosition in tilemapBounds.allPositionsWithin)
        {
            if(coinTilemap.HasTile(tilemapPosition)) coinAmount++;
        }

        //Compensate for the starting value of 1 (bypass of CheckWinConditions())
        coinAmount--;
    }
}
