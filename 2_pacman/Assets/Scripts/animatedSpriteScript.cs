using UnityEngine;

public class animatedSpriteScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; 
    private int spritesArrayIndex;
    [SerializeField] Sprite[] spritesArray;
    private float spriteChangeTime = .15f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(ChangeSprite), spriteChangeTime, spriteChangeTime);
    }

    private void ChangeSprite()
    {
        spritesArrayIndex++;
        var wentThroughAllArray = spritesArrayIndex >= spritesArray.Length;
        if(wentThroughAllArray) spritesArrayIndex = 0;
        spriteRenderer.sprite = spritesArray[spritesArrayIndex];
    }
}