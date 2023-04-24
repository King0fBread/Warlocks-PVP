using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    [SerializeField] private Slider _leftPlayerHealthBar;
    [SerializeField] private Slider _rightPlayerHealthBar;
    private int _leftHealthValue = 15;
    private int _rightHealthValue = 15;

    public static HealthBars Instance;
    private void Awake()
    {
        Instance = this;

        _leftPlayerHealthBar.value = _leftHealthValue;
        _rightPlayerHealthBar.value = _rightHealthValue;
    }
}
