using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEventArgs : EventArgs
{
    public Card SelectedCardObject { get; private set; }
    public CardEventArgs(Card selectedCardObject)
    {
        SelectedCardObject = selectedCardObject;
    }
}
