using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsContainer : MonoBehaviour
{
    public Card[] AvailableCardsArray = new Card[8];

    public static CardsContainer Instance;
    private void Awake()
    {
        Instance = this;
    }
}
