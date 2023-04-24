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
    private bool _previousPlayerWasLeft;
    private void Start()
    {
        CoinToss.Instance.OnCoinTossed += DecideFirstAttacker_OnCoinTossed;
    }

    private void DecideFirstAttacker_OnCoinTossed(object sender, int e)
    {
        if (e == 0)
        {
            BeginLeftPlayerAttackExucution();
            _previousPlayerWasLeft = true;
        }
        else if (e == 1)
        {
            BeginRightPlayerAttackExecution();
            _previousPlayerWasLeft = false;
        }
    }
    public void DecideAttackerForNonFirstRound()
    {
        if (_previousPlayerWasLeft)
        {
            BeginRightPlayerAttackExecution();
        }
        else if (!_previousPlayerWasLeft)
        {
            BeginLeftPlayerAttackExucution();
        }
    }
    private void BeginLeftPlayerAttackExucution()
    {
        _attackIndex++;
        StartCoroutine(ExecuteAttack(_playerDeckList.GetLeftDeckList(), _leftDeckAttackEffects, true));
    }
    private void BeginRightPlayerAttackExecution()
    {
        _attackIndex++;
        StartCoroutine(ExecuteAttack(_playerDeckList.GetRightDeckList(), _rightDeckAttackEffects, false));
    }
    private IEnumerator ExecuteAttack(List<Card> deckList, AttackVisualEffect[] attackEffects, bool leftPlayerAttacking)
    {
        int i = 0;
        foreach (Card card in deckList)
        {
            if(card.PoisonAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(0, card.PoisonAmount);
                yield return new WaitForSeconds(1.5f);
            }
            if(card.HealAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(1, card.HealAmount);
                yield return new WaitForSeconds(1.5f);
            }
            if(card.AttackAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(2, card.AttackAmount);
                yield return new WaitForSeconds(1.5f);
            }
            if(card.LifestealAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(3, card.LifestealAmount);
                yield return new WaitForSeconds(1.5f);
            }
            i++;
        }

        //switch to the next player attack or finish if both have attacked
        if (leftPlayerAttacking)
        {
            if (_attackIndex == 2)
            {
                _attackIndex = 0;
                OnEachPlayerHasAttacked?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                BeginRightPlayerAttackExecution();
            }
        }
        else
        {
            if (_attackIndex == 2)
            {
                _attackIndex = 0;
                OnEachPlayerHasAttacked?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                BeginLeftPlayerAttackExucution();
            }
        }
    }

    
}
