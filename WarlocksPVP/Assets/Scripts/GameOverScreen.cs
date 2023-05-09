using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private Image _backgroundImageHolder;
    [SerializeField] private TextMeshProUGUI _winTextHolder;

    [SerializeField] private Sprite _leftPlayerWinBackground;
    [SerializeField] private Sprite _rightPlayerWinBackground;

    public static GameOverScreen Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void DisplayWinScreen(string winText, bool leftPlayerWon)
    {
        _winScreen.SetActive(true);
        _winTextHolder.text = winText;
        if (leftPlayerWon)
            _backgroundImageHolder.sprite = _leftPlayerWinBackground;
        else
            _backgroundImageHolder.sprite = _rightPlayerWinBackground;
    }
}
