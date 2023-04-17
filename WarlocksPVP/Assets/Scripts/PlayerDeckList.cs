using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerDeckList : NetworkBehaviour
{
    private List<Card> _leftPlayerList = new List<Card>();
    private List<Card> _rightPlayerList = new List<Card>();
    private void Start()
    {
        CardSlotsAssigner.Instance.OnCardSelected += AddCardToList_OnCardSelected;
    }

    private void AddCardToList_OnCardSelected(object sender, CardEventArgs e)
    {
        if (!IsServer)
            return;

        //logic
        Card card = e.SelectedCardObject;
    }
}
