using UnityEngine;
using UnityEngine.Tilemaps;

public class coinScript : MonoBehaviour
{
    private Tilemap coinTilemap;
    [SerializeField] Sprite coinSprite;
    public int coinAmount;
    private gameManagerScript gameManagerScript;

    private void Awake()
    {
        coinTilemap = this.gameObject.GetComponent<Tilemap>();
        gameManagerScript = GetComponent<gameManagerScript>();
    }

    private void Start()
    {
        coinAmount++;
        //CheckStartingGameCoinAmount();
    }
/*
    private void CheckStartingGameCoinAmount()
    {
        BoundsInt tilemapBounds = coinTilemap.cellBounds;
        
        foreach(Vector3Int tilemapPosition in tilemapBounds.allPositionsWithin)
        {
            Tile tileToCheck = coinTilemap.GetTile<Tile>(tilemapPosition);
            Debug.Log(tileToCheck.sprite.name);
            //if(tileToCheck.sprite == coinSprite) coinAmount++;
        }
    }
*/
    public void PlayerHitCoin()
    {
        coinAmount--;
    }
}
