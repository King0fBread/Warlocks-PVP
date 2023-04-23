using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsAttackExecution : MonoBehaviour
{
    public event EventHandler OnEachPlayerHasAttacked;

    [SerializeField] private Sprite AttackSprite;
    [SerializeField] private Sprite PoisonSprite;
    [SerializeField] private Sprite HealSprite;
    [SerializeField] private Sprite LifestealSprite;

    [SerializeField] private AttackVisualEffect[] _leftDeckAttackEffects = new AttackVisualEffect[4];
    [SerializeField] private AttackVisualEffect[] _rightDeckAttackEffects = new AttackVisualEffect[4];

    [SerializeField] private PlayerDeckList _playerDeckList;

    private int _attackIndex = 0;
    public void BeginLeftPlayerAttackExucution()
    {
        _attackIndex++;
        StartCoroutine(ExecuteAttack(_playerDeckList.GetLeftDeckList(), _leftDeckAttackEffects));

        if(_attackIndex == 2)
        {
            _attackIndex = 0;
            OnEachPlayerHasAttacked?.Invoke(this, EventArgs.Empty);
        }
    }
    public void BeginRightPlayerAttackExecution()
    {
        _attackIndex++;
        StartCoroutine(ExecuteAttack(_playerDeckList.GetRightDeckList(), _rightDeckAttackEffects));

        if(_attackIndex == 2)
        {
            _attackIndex = 0;
            OnEachPlayerHasAttacked?.Invoke(this, EventArgs.Empty);
        }
    }
    private IEnumerator ExecuteAttack(List<Card> deckList, AttackVisualEffect[] attackEffects)
    {
        int i = 0;
        foreach (Card card in deckList)
        {
            if(card.PoisonAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(0, card.PoisonAmount);
                yield return new WaitForSeconds(2f);
            }
            if(card.HealAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(1, card.HealAmount);
                yield return new WaitForSeconds(2f);
            }
            if(card.AttackAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(2, card.AttackAmount);
                yield return new WaitForSeconds(2f);
            }
            if(card.LifestealAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(3, card.LifestealAmount);
                yield return new WaitForSeconds(2f);
            }
        }
    }

    
}
