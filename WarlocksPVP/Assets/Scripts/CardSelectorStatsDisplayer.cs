using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectorStatsDisplayer : MonoBehaviour
{
    [SerializeField] private int _cardsArrayId;
    [SerializeField] private string _cardName;
    [SerializeField] private int _attackAmount;
    [SerializeField] private int _healAmount;
    [SerializeField] private int _poisonAmount;
    [SerializeField] private int _lifestealAmount;
    private Sprite _cardSprite;
    private void Awake()
    {
        _cardSprite = GetComponent<SpriteRenderer>().sprite;
    }
    private void Start()
    {
        //populating the list of available cards for future referencing
        CardsContainer.Instance.AvailableCardsArray[_cardsArrayId] = new Card(_cardSprite, _cardName, _attackAmount, _healAmount, _poisonAmount, _lifestealAmount, _cardsArrayId);
    }

    private void OnMouseEnter()
    {
        CardStatsObject.Instance.Show(_cardSprite, _cardName, _attackAmount.ToString(), _healAmount.ToString(), _poisonAmount.ToString(), _lifestealAmount.ToString());
    }
    private void OnMouseExit()
    {
        CardStatsObject.Instance.Hide();
    }
    private void OnMouseDown()
    {
        CardSlotsAssigner.Instance.AddToDeckAndDisplayCardOnAvailableHolder(CardsContainer.Instance.AvailableCardsArray[_cardsArrayId]);
        
    }
}
