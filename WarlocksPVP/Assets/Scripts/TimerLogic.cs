using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class TimerLogic : NetworkBehaviour 
{
    [SerializeField] private int _maxTimerValueInSeconds;
    private Slider _timerSlider;
    private bool _timerEnabled = false;
    private float _timerValue;
    private void Awake()
    {
        _timerSlider = GetComponent<Slider>();
        _timerValue = _maxTimerValueInSeconds;
    }
    private void Start()
    {
        PlayerRoomTransitions.Instance.OnSwitchedToDeckRoom += EnableTimer_OnSwitchedToDeckRoom;
    }

    private void EnableTimer_OnSwitchedToDeckRoom(object sender, System.EventArgs e)
    {
        _timerEnabled = true;
        _timerValue = _maxTimerValueInSeconds;
    }

    private void Update()
    {
        if (!IsServer)
            return;

        if (_timerEnabled)
        {
            DisplayTimerClientRpc();
        }
    }
    [ClientRpc]
    private void DisplayTimerClientRpc()
    {
        if (_timerValue > 0)
        {
            _timerValue -= Time.deltaTime;
            _timerSlider.value = _timerValue / _maxTimerValueInSeconds;
        }
        else
        {
            _timerEnabled = false;
            Debug.Log("timer finished");
        }
    }
}
