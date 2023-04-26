using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlotsAssigner : MonoBehaviour
{
    public event EventHandler<CardEventArgs> OnCardSelected;
    public event EventHandler OnDeckCleared;

    [SerializeField] private CardHolder[] _cardHolders;
    [SerializeField] private GameObject _sameCardReusedWarning;
    private int _currentIndex = 0;

    public static CardSlotsAssigner Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PlayerRoomTransitions.Instance.OnSwitchedToDeckRoom += ClearHolders_OnSwitchedToDeckRoom;
    }

    private void ClearHolders_OnSwitchedToDeckRoom(object sender, System.EventArgs e)
    {
        _currentIndex = 0;
        foreach (CardHolder holder in _cardHolders)
        {
            holder.ClearHolder();
        }
    }
    public void AddToDeckAndDisplayCardOnAvailableHolder(Card card)
    {
        if(_currentIndex < 4)
        {
            foreach(CardHolder holder in _cardHolders)
            {
                if (holder.GetHolderSpriteRenderer().sprite == card.CardSprite)
                {
                    _sameCardReusedWarning.SetActive(true);
                    return;
                }
            }

            _cardHolders[_currentIndex].DisplayCard(card.CardSprite);
            _currentIndex++;
            OnCardSelected?.Invoke(this, new CardEventArgs(card));
        }

    }
    public void ClearHolders()
    {
        _currentIndex = 0;
        foreach(CardHolder holder in _cardHolders)
        {
            holder.ClearHolder();
        }
        OnDeckCleared?.Invoke(this, EventArgs.Empty);
    }
}
