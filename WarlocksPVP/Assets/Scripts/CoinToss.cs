using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CoinToss : NetworkBehaviour 
{
    public event EventHandler<int> OnCoinTossed;

    public static CoinToss Instance;

    [SerializeField] private GameObject _coinAnimationObject;
    [SerializeField] private CardsAttackExecution _cardsAttackExecution;
    private bool _coinHasBeenTossed = false;
    private string[] _coinTossAnimations = new string[2];
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _coinTossAnimations[0] = "CoinflipLeft";
        _coinTossAnimations[1] = "CoinflipRight";
        PlayerRoomTransitions.Instance.OnSwitchedToArenaRoom += TryCoinToss_OnSwitchedToArenaRoom;
    }

    private void TryCoinToss_OnSwitchedToArenaRoom(object sender, System.EventArgs e)
    {
        if (IsServer && !_coinHasBeenTossed)
        {
            _coinHasBeenTossed = true;
            int randomCoinAnimationIndex = UnityEngine.Random.Range(0, 2);
            PlayCoinAnimationClientRpc(randomCoinAnimationIndex);
        }
    }
    [ClientRpc]
    private void PlayCoinAnimationClientRpc(int animationIndex)
    {
        _coinAnimationObject.SetActive(true);
        _coinAnimationObject.transform.GetChild(0).GetComponent<Animator>().Play(_coinTossAnimations[animationIndex]);

        StartCoroutine(DelayedAttackSequenceCoroutine(animationIndex));
    }
    private IEnumerator DelayedAttackSequenceCoroutine(int animationIndex)
    {
        yield return new WaitForSeconds(4f);
        OnCoinTossed?.Invoke(this, animationIndex);

        PlayersHealthBars.Instance.AddExtraHealthToCoinTossWinner(animationIndex == 0);
        PlayerRoomTransitions.Instance.OnSwitchedToArenaRoom -= TryCoinToss_OnSwitchedToArenaRoom;
        Destroy(gameObject);
    }
}
