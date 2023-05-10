using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsAttackExecution : MonoBehaviour
{
    [SerializeField] private Sprite AttackSprite;
    [SerializeField] private Sprite PoisonSprite;
    [SerializeField] private Sprite HealSprite;
    [SerializeField] private Sprite LifestealSprite;

    [SerializeField] private AttackVisualEffect[] _leftDeckAttackEffects = new AttackVisualEffect[4];
    [SerializeField] private AttackVisualEffect[] _rightDeckAttackEffects = new AttackVisualEffect[4];

    [SerializeField] private PlayerDeckList _playerDeckList;

    public static CardsAttackExecution Instance;

    private List<Card> _leftList;
    private List<Card> _rightList;

    private int _attackIndex = 0;
    private bool _previousPlayerWasLeft;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CoinToss.Instance.OnCoinTossed += DecideFirstAttacker_OnCoinTossed;
        PlayerRoomTransitions.Instance.OnSwitchedToArenaRoom += GetPlayerDecks_OnSwitchedToArenaRoom;
    }

    private void GetPlayerDecks_OnSwitchedToArenaRoom(object sender, EventArgs e)
    {
        _leftList = _playerDeckList.GetLeftDeckList();
        _rightList = _playerDeckList.GetRightDeckList();
    }

    private void DecideFirstAttackerForNonFirstRound_OnSwitchedToArenaRoom(object sender, EventArgs e)
    {
        DecideAttackerForNonFirstRound();
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

        PlayerRoomTransitions.Instance.OnSwitchedToArenaRoom += DecideFirstAttackerForNonFirstRound_OnSwitchedToArenaRoom;
        CoinToss.Instance.OnCoinTossed -= DecideFirstAttacker_OnCoinTossed;
    }
    public void DecideAttackerForNonFirstRound()
    {
        if (_previousPlayerWasLeft)
        {
            _previousPlayerWasLeft = false;
            BeginRightPlayerAttackExecution();
        }
        else if (!_previousPlayerWasLeft)
        {
            _previousPlayerWasLeft = true;
            BeginLeftPlayerAttackExucution();
        }
    }
    private void BeginLeftPlayerAttackExucution()
    {
        _attackIndex++;
        StartCoroutine(ExecuteAttack(_leftList, _leftDeckAttackEffects, true));
    }
    private void BeginRightPlayerAttackExecution()
    {
        _attackIndex++;
        StartCoroutine(ExecuteAttack(_rightList, _rightDeckAttackEffects, false));
    }
    private IEnumerator ExecuteAttack(List<Card> deckList, AttackVisualEffect[] attackEffects, bool leftPlayerAttacking)
    {
        for(int i = 0; i<=3; i++)
        {
            if(deckList.Count < 4 || i > 3)
            {
                break;
            }
            if(deckList[i].PoisonAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(0, deckList[i].PoisonAmount);
                deckList[i].Poison(!leftPlayerAttacking);
                yield return new WaitForSeconds(1.5f);
            }
            if(deckList[i].HealAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(1, deckList[i].HealAmount);
                deckList[i].Heal(leftPlayerAttacking);
                yield return new WaitForSeconds(1.5f);
            }
            if(deckList[i].AttackAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(2, deckList[i].AttackAmount);
                deckList[i].Attack(!leftPlayerAttacking);
                yield return new WaitForSeconds(1.5f);
            }
            if(deckList[i].LifestealAmount > 0)
            {
                attackEffects[i].gameObject.SetActive(true);
                attackEffects[i].DisplayAttackStats(3, deckList[i].LifestealAmount);
                deckList[i].Lifesteal(!leftPlayerAttacking);
                yield return new WaitForSeconds(1.5f);
            }
        }

        //switch to the next player attack or finish if both have attacked
        if (leftPlayerAttacking)
        {
            if (_attackIndex == 2)
            {
                _attackIndex = 0;
                PlayerRoomTransitions.Instance.MoveBothPlayersToDeckRoomServerRpc();
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
                PlayerRoomTransitions.Instance.MoveBothPlayersToDeckRoomServerRpc();
            }
            else
            {
                BeginLeftPlayerAttackExucution();
            }
        }
    }

    
}
