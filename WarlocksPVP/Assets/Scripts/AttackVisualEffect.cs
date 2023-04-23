using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackVisualEffect : MonoBehaviour
{
    [SerializeField] private Sprite[] _effectSprites = new Sprite[4];
    private SpriteRenderer _effectSpriteRenderer;
    private TextMeshProUGUI _effectValueText;

    private float _secondsForDisplaying;
    private bool _canDisplayStats;
    private void Awake()
    {
        _effectValueText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _effectSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
    }
    public void DisplayAttackStats(int effectSpriteIndex, int effectValue)
    {
        _effectSpriteRenderer.sprite = _effectSprites[effectSpriteIndex];
        _effectValueText.text = effectValue.ToString();

        _secondsForDisplaying = 2;
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
