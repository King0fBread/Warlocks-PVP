using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerDeckList : NetworkBehaviour
{
    public PlayerDeckList Instance;

    [SerializeField] private SpriteRenderer[] _leftPlayerCardSpriteRenderers = new SpriteRenderer[4];
    [SerializeField] private SpriteRenderer[] _rightPlayerCardSpriteRenderers = new SpriteRenderer[4];

    private List<Card> _leftPlayerDeckList = new List<Card>();
    private List<Card> _rightPlayerDeckList = new List<Card>();

    private bool _playerIsHost;
    private void Awake()
    {
        Instance = this;
    }
    public override void OnNetworkSpawn()
    {
        _playerIsHost = IsServer ? true : false;
    }
    private void Start()
    {
        CardSlotsAssigner.Instance.OnCardSelected += AddCardToList_OnCardSelected;
        CardSlotsAssigner.Instance.OnDeckCleared += ClearDeckList_OnDeckCleared;
        PlayerRoomTransitions.Instance.OnSwitchedToArenaRoom += DisplayPlayerDecks_OnSwitchedToArenaRoom;
        PlayerRoomTransitions.Instance.OnSwitchedToDeckRoom += ClearBothLists_OnSwitchedToDeckRoom;
    }

    private void ClearBothLists_OnSwitchedToDeckRoom(object sender, System.EventArgs e)
    {
        ClearBothListsServerRpc();
    }

    private void DisplayPlayerDecks_OnSwitchedToArenaRoom(object sender, System.EventArgs e)
    {
        DisplayPlayerDecksServerRpc();
    }

    private void ClearDeckList_OnDeckCleared(object sender, System.EventArgs e)
    {
        ClearDeckListServerRpc(_playerIsHost);
    }

    private void AddCardToList_OnCardSelected(object sender, CardEventArgs e)
    {
        int selectedCardId = e.SelectedCardObject.GetArrayId();
        AddCardToListServerRpc(selectedCardId, _playerIsHost);
    }
    public List<Card> GetLeftDeckList()
    {
        return _leftPlayerDeckList;
    }
    public List<Card> GetRightDeckList()
    {
        return _rightPlayerDeckList;
    }

    [ServerRpc (RequireOwnership = false)]
    private void AddCardToListServerRpc(int cardId, bool isHost)
    {
        AddCardToListClientRpc(cardId, isHost);
    }
    [ClientRpc]
    private void AddCardToListClientRpc(int cardId, bool isHost)
    {
        if (isHost)
        {
            _leftPlayerDeckList.Add(CardsContainer.Instance.AvailableCardsArray[cardId]);
        }
        else
        {
            _rightPlayerDeckList.Add(CardsContainer.Instance.AvailableCardsArray[cardId]);
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
        }
        else
        {
            _rightPlayerDeckList.Clear();
        }
    }
    [ServerRpc (RequireOwnership = false)]
    private void DisplayPlayerDecksServerRpc()
    {
        DisplayPlayerDecksClientRpc();
    }
    [ClientRpc]
    private void DisplayPlayerDecksClientRpc()
    {
        int indexLeft = 0;
        int indexRight = 0;

        foreach(Card card in _leftPlayerDeckList)
        {
            _leftPlayerCardSpriteRenderers[indexLeft].sprite = card.CardSprite;
            indexLeft++;
        }
        foreach(Card card in _rightPlayerDeckList)
        {
            _rightPlayerCardSpriteRenderers[indexRight].sprite = card.CardSprite;
            indexRight++;
        }

    }
    [ServerRpc (RequireOwnership = false)]
    private void ClearBothListsServerRpc()
    {
        ClearBothListsClientRpc();
    }
    [ClientRpc]
    private void ClearBothListsClientRpc()
    {
        _leftPlayerDeckList.Clear();
        _rightPlayerDeckList.Clear();
        print("cleared both lists");
    }

}
