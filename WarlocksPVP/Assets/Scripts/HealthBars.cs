using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBars : MonoBehaviour
{
    [SerializeField] private Slider _leftPlayerHealthBar;
    [SerializeField] private Slider _rightPlayerHealthBar;

    [SerializeField] private TextMeshProUGUI _leftHealthValueDisplayer;
    [SerializeField] private TextMeshProUGUI _rightHealthValueDisplayer;
    private int _leftHealthValue = 15;
    private int _rightHealthValue = 15;

    public static HealthBars Instance;
    private void Awake()
    {
        Instance = this;

        _leftPlayerHealthBar.value = _leftHealthValue;
        _rightPlayerHealthBar.value = _rightHealthValue;
    }
    private void Update()
    {
        _leftHealthValueDisplayer.text = Mathf.RoundToInt(_leftPlayerHealthBar.value).ToString();
        _rightHealthValueDisplayer.text = Mathf.RoundToInt(_rightPlayerHealthBar.value).ToString();
    }
    public Slider GetLeftHealthBar()
    {
        return _leftPlayerHealthBar;
    }
    public Slider GetRightHealthBar()
    {
        return _rightPlayerHealthBar;
    }
}
