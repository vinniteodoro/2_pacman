using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class animatedSpriteScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] spritesArray;
    private float spriteChangeTime = .15f;
    public int spritesArrayIndex { get; private set; }
    public bool isSpriteDynamic = true;

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

        if(spritesArrayIndex >= spritesArray.Length && isSpriteDynamic)
        {
            spritesArrayIndex = 0;
        }

        spriteRenderer.sprite = spritesArray[spritesArrayIndex];
    }
}