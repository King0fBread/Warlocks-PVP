using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayersHealthBars : MonoBehaviour
{
    [SerializeField] private Slider _leftPlayerHealthBar;
    [SerializeField] private Slider _rightPlayerHealthBar;

    [SerializeField] private TextMeshProUGUI _leftHealthValueDisplayer;
    [SerializeField] private TextMeshProUGUI _rightHealthValueDisplayer;

    [Header("Indicators")]
    [SerializeField] private GameObject _leftHelathUpIndicator;
    [SerializeField] private GameObject _rightHealthUpIndicator;
    [SerializeField] private GameObject _leftHealthDownIndicator;
    [SerializeField] private GameObject _rightHealthDownIndicator;

    private int _leftHealthValue = 25;
    private int _rightHealthValue = 25;

    private bool _leftCanHoldExtraHealth;
    private bool _rightCanHoldExtraHealth;

    private bool _endScreenOn = false;

    public static PlayersHealthBars Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        _leftPlayerHealthBar.value = _leftHealthValue;
        _rightPlayerHealthBar.value = _rightHealthValue;

        _leftHealthValueDisplayer.text = _leftHealthValue.ToString();
        _rightHealthValueDisplayer.text = _rightHealthValue.ToString();

        if (_leftHealthValue <= 0 && !_endScreenOn)
        {
            GameOverScreen.Instance.DisplayWinScreen("purple warlock wins!", false);
            _endScreenOn = true;
        }
        if (_rightHealthValue <= 0 && !_endScreenOn)
        {
            GameOverScreen.Instance.DisplayWinScreen("orange warlock wins!", true);
            _endScreenOn = true;
        }
    }   
    public void DecreaseHealthValue(bool leftPlayerAffected, int amount)
    {
        if (leftPlayerAffected)
        {
            _leftHealthValue -= amount;
            _leftHealthDownIndicator.SetActive(true);

            if (_leftHealthValue <= 25)
                _leftCanHoldExtraHealth = false;
        }
        else
        {
            _rightHealthValue -= amount;
            _rightHealthDownIndicator.SetActive(true);

            if(_rightHealthValue <= 25)
            {
                _rightCanHoldExtraHealth = false;
            }
        }
    }
    public void IncreaseHealthValue(bool leftPlayerAffected, int amount)
    {
        if (leftPlayerAffected)
        {
            _leftHealthValue += amount;
            _leftHelathUpIndicator.SetActive(true);

            if (_leftHealthValue > 25 && !_leftCanHoldExtraHealth)
                _leftHealthValue = 25;
            else if (_leftHealthValue > 30)
                _leftHealthValue = 30;
        }
        else
        {
            _rightHealthValue += amount;
            _rightHealthUpIndicator.SetActive(true);

            if (_rightHealthValue > 25 && !_rightCanHoldExtraHealth)
                _rightHealthValue = 25;
            else if (_rightHealthValue > 30)
                _rightHealthValue = 30;
        }
    }
    public void AddExtraHealthToCoinTossWinner(bool leftPlayerAffected)
    {
        if (leftPlayerAffected)
        {
            _leftHealthValue += 5;
            _leftCanHoldExtraHealth = true;
        }
        else
        {
            _rightHealthValue += 5;
            _rightCanHoldExtraHealth = true;
        }
    }
}
