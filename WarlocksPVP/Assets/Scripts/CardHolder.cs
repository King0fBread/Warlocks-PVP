using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    private SpriteRenderer _cardDisplaySpriteRenderer;

    private void Awake()
    {
        _cardDisplaySpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    public void DisplayCard(Sprite cardSprite)
    {
        _cardDisplaySpriteRenderer.sprite = cardSprite;
    }
    public void ClearHolder()
    {
        _cardDisplaySpriteRenderer.sprite = null;
    }
    public SpriteRenderer GetHolderSpriteRenderer()
    {
        return _cardDisplaySpriteRenderer;
    }

}
