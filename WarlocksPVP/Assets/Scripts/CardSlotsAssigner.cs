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
    public void DisplayCardOnAvailableHolder(Sprite cardSprite)
    {
        _cardHolders[_currentIndex].DisplayCard(cardSprite);
        _currentIndex++;

    }
}
