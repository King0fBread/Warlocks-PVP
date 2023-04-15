using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class TimerLogic : NetworkBehaviour 
{
    [SerializeField] private int _maxTimerValueInSeconds;
    private Slider _timerSlider;
    private Image _backgroundImage;
    private bool _timerEnabled = false;
    private float _timerValue;
    private Color _visibleColorAlpha;
    private Color _hiddenColorAlpha;
    private void Awake()
    {
        _timerSlider = GetComponent<Slider>();
        _backgroundImage = transform.GetChild(0).GetComponent<Image>();
        _timerValue = _maxTimerValueInSeconds;

        _visibleColorAlpha = new Color(1f, 1f, 1f, 1f);
        _hiddenColorAlpha = new Color(1f, 1f, 1f, 0f);
    }
    private void Start()
    {
        PlayerRoomTransitions.Instance.OnSwitchedToDeckRoom += EnableTimer_OnSwitchedToDeckRoom;
    }

    private void EnableTimer_OnSwitchedToDeckRoom(object sender, System.EventArgs e)
    {
        _timerEnabled = true;
        _timerValue = _maxTimerValueInSeconds;
        _backgroundImage.color = _visibleColorAlpha;
    }

    private void Update()
    {
        if (!IsServer)
            return;

        if (_timerEnabled)
        {
            _timerValue -= Time.deltaTime;
            DisplayTimerClientRpc(_timerValue);
        }
    }
    [ClientRpc]
    private void DisplayTimerClientRpc(float timerValue)
    {
        if (timerValue > 0)
        {
            _timerSlider.value = timerValue / _maxTimerValueInSeconds;
        }
        else
        {
            _timerEnabled = false;
            _backgroundImage.color = _hiddenColorAlpha;
            Debug.Log("timer finished");
        }
    }
}
