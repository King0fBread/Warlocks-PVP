using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerDeckList : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer[] _leftPlayerCardSpriteRenderers = new SpriteRenderer[4];
    [SerializeField] private SpriteRenderer[] _rightPlayerCardSpriteRenderers = new SpriteRenderer[4];

    private List<Card> _leftPlayerDeckList = new List<Card>();
    private List<Card> _rightPlayerDeckList = new List<Card>();

    private bool _playerIsHost;
    public override void OnNetworkSpawn()
    {
        _playerIsHost = _playerIsHost == IsServer ? true : false;
    }
    private void Start()
    {
        print("started deck list");
        CardSlotsAssigner.Instance.OnCardSelected += AddCardToList_OnCardSelected;
        CardSlotsAssigner.Instance.OnDeckCleared += ClearDeckList_OnDeckCleared;
    }

    private void ClearDeckList_OnDeckCleared(object sender, System.EventArgs e)
    {
        ClearDeckListServerRpc(_playerIsHost);
    }

    private void AddCardToList_OnCardSelected(object sender, CardEventArgs e)
    {
        print("event invoked");
        AddCardToListServerRpc(e.SelectedCardObject, _playerIsHost);
    }

    [ServerRpc (RequireOwnership = false)]
    private void AddCardToListServerRpc(Card card, bool isHost)
    {
        print("server rpc invoked");
        AddCardToListClientRpc(card, isHost);
    }
    [ClientRpc]
    private void AddCardToListClientRpc(Card card, bool isHost)
    {
        if (isHost)
        {
            _leftPlayerDeckList.Add(card);
            print("added to left list");
        }
        else
        {
            _rightPlayerDeckList.Add(card);
            print("added to right list");
        }
    }
    [ServerRpc (RequireOwnership = false)]
    private void ClearDeckListServerRpc(bool isHost)
    {
        ClearDeckListClientRpc(isHost);
    }
    [ClientRpc]
    private void ClearDeckListClientRpc(bool isHost)
    {
        if (isHost)
        {
            _leftPlayerDeckList.Clear();
            print("cleared left list");
        }
        else
        {
            _rightPlayerDeckList.Clear();
            print("cleared right list");
        }
    }
}
