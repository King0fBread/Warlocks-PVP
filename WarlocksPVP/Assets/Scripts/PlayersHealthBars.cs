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

    private int _leftHealthValue = 20;
    private int _rightHealthValue = 20;

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
    }
    public void DecreaseHealthValue(bool leftPlayerAffected, int amount)
    {
        if (leftPlayerAffected)
        {
            _leftHealthValue -= amount;
            _leftHealthDownIndicator.SetActive(true);
        }
        else
        {
            _rightHealthValue -= amount;
            _rightHealthDownIndicator.SetActive(true);
        }
    }
    public void IncreaseHealthValue(bool leftPlayerAffected, int amount)
    {
        if (leftPlayerAffected)
        {
            _leftHealthValue += amount;
            _leftHelathUpIndicator.SetActive(true);
            if (_leftHealthValue > 20)
                _leftHealthValue = 20;
        }
        else
        {
            _rightHealthValue += amount;
            _rightHealthUpIndicator.SetActive(true);
            if (_rightHealthValue > 20)
                _rightHealthValue = 20;
        }
    }
}
