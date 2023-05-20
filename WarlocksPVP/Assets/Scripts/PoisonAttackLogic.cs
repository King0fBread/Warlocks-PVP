using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class PoisonAttackLogic : NetworkBehaviour
{
    public static PoisonAttackLogic Instance;

    [SerializeField] private SpriteRenderer _leftPlayerPoisonIcon;
    [SerializeField] private SpriteRenderer _rightPlayerPoisonIcon;

    [SerializeField] private TextMeshProUGUI _leftAmountText;
    [SerializeField] private TextMeshProUGUI _rightAmountText;

    private int _currentLeftPoisonAmount;
    private int _currentRightPosionAmount;
    private void Awake()
    {
        Instance = this;

        _leftPlayerPoisonIcon.gameObject.SetActive(false);
        _rightPlayerPoisonIcon.gameObject.SetActive(false);
    }
    private void Start()
    {
        PlayerRoomTransitions.Instance.OnSwitchedToArenaRoom += ApplyExistingPoison_OnSwitchedToArenaRoom;
    }
    public void AddPoisonToPlayer(bool affectLeftPlayer, int poisonAmount)
    {
        AddPoisonToPlayerClientRpc(affectLeftPlayer, poisonAmount);
    }
    private void ApplyExistingPoison_OnSwitchedToArenaRoom(object sender, System.EventArgs e)
    {
        if (IsServer)
        {
            ApplyExistingPoisonClientRpc();
        }
    }
    [ClientRpc]
    private void AddPoisonToPlayerClientRpc(bool affectLeftPlayer, int poisonAmount)
    {
        if (affectLeftPlayer)
        {
            _leftPlayerPoisonIcon.gameObject.SetActive(true);
            int previousAmount = int.Parse(_leftAmountText.text);
            _leftAmountText.text = (previousAmount + poisonAmount).ToString();
            _currentLeftPoisonAmount = previousAmount + poisonAmount;
        }
        else
        {
            _rightPlayerPoisonIcon.gameObject.SetActive(true);
            int previousAmount = int.Parse(_rightAmountText.text);
            _rightAmountText.text = (previousAmount + poisonAmount).ToString();
            _currentRightPosionAmount = previousAmount + poisonAmount;
        }
    }
    [ClientRpc]
    private void ApplyExistingPoisonClientRpc()
    {
        if (_currentLeftPoisonAmount > 0)
        {
            PlayersHealthBars.Instance.DecreaseHealthValue(true, _currentLeftPoisonAmount);

            _currentLeftPoisonAmount = 0;
            _leftAmountText.text = "0";
            _leftPlayerPoisonIcon.gameObject.SetActive(false);
        }
        if (_currentRightPosionAmount > 0)
        {
            PlayersHealthBars.Instance.DecreaseHealthValue(false, _currentRightPosionAmount);

            _currentRightPosionAmount = 0;
            _rightAmountText.text = "0";
            _rightPlayerPoisonIcon.gameObject.SetActive(false);
        }
    }
}
