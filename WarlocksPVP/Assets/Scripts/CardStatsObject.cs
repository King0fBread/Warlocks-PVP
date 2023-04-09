using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardStatsObject : MonoBehaviour
{
    public static CardStatsObject Instance { get; private set; }
    [SerializeField] private Image _cardImage;
    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _attackAmount;
    [SerializeField] private TextMeshProUGUI _healAmount;
    [SerializeField] private TextMeshProUGUI _poisonAmount;
    [SerializeField] private TextMeshProUGUI _lifestealAmount;
    private void Awake()
    {
        Instance = this;
        Hide();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show(Sprite sprite, string name, string attack, string heal, string poison, string lifesteal)
    {
        _cardImage.sprite = sprite;
        _cardName.text = name;
        _attackAmount.text = attack;
        _healAmount.text = heal;
        _poisonAmount.text = poison;
        _lifestealAmount.text = lifesteal;

        gameObject.SetActive(true);
    }
}
