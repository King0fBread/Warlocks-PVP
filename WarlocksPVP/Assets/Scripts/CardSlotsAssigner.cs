using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlotsAssigner : MonoBehaviour
{
    [SerializeField] private CardHolder[] _cardHolders;
    private int _currentIndex = 0;

    public static CardSlotsAssigner Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void AddAndDisplayCardOnAvailableHolder(Card card)
    {
        _cardHolders[_currentIndex].DisplayCard(card.GetCardSprite());
        _currentIndex++;

    }
}
