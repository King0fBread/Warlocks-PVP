using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackVisualEffect : MonoBehaviour
{
    [SerializeField] private Sprite[] _effectSprites = new Sprite[4];
    private Image _effectSpriteHolder;
    private TextMeshProUGUI _effectValueText;

    private float _secondsForDisplaying;
    private bool _canDisplayStats;
    private void Awake()
    {
        _effectValueText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _effectSpriteHolder = transform.GetChild(1).GetComponent<Image>();

        gameObject.SetActive(false);
    }
    public void DisplayAttackStats(int effectSpriteIndex, int effectValue)
    {
        _effectSpriteHolder.sprite = _effectSprites[effectSpriteIndex];
        _effectValueText.text = effectValue.ToString();

        _secondsForDisplaying = 1.5f;
        _canDisplayStats = true;
    }
    private void Update()
    {
        if(_canDisplayStats && _secondsForDisplaying > 0)
        {
            _secondsForDisplaying -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        _canDisplayStats = false;
    }
}
