using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CoinToss : NetworkBehaviour 
{
    [SerializeField] private GameObject _coinAnimationObject;
    [SerializeField] private CardsAttackExecution _cardsAttackExecution;
    private bool _coinHasBeenTossed = false;
    private string[] _coinTossAnimations = new string[2];
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
            int randomCoinAnimationIndex = Random.Range(0, 2);
            PlayCoinAnimationClientRpc(randomCoinAnimationIndex);
        }
    }
    [ClientRpc]
    private void PlayCoinAnimationClientRpc(int animationIndex)
    {
        _coinAnimationObject.SetActive(true);
        _coinAnimationObject.transform.GetChild(0).GetComponent<Animator>().Play(_coinTossAnimations[animationIndex]);

        //sets the initial starting player, based on cointoss result
        _cardsAttackExecution._isLeftPlayerTurn = animationIndex == 0;
    }
}
