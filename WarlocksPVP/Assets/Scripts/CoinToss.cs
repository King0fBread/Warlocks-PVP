using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CoinToss : NetworkBehaviour 
{
    [SerializeField] private GameObject _coinAnimationObject;
    [SerializeField] private AnimationClip[] _coinTossAnimations;
    private bool _coinHasBeenTossed = false;
    private void Start()
    {
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
        _coinAnimationObject.transform.GetChild(0).GetComponent<Animator>().Play(_coinTossAnimations.ToString());
    }
}
