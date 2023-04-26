using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Card
{
    public Sprite CardSprite;
    public string CardName;
    public int AttackAmount;
    public int HealAmount;
    public int PoisonAmount;
    public int LifestealAmount;

    private int _arrayId;

    public Card(Sprite CardSprite, string CardName, int AttackAmount, int HealAmount, int PoisonAmount, int LifestealAmount, int arrayId)
    {
        this.CardSprite = CardSprite;
        this.CardName = CardName;
        this.AttackAmount = AttackAmount;
        this.HealAmount = HealAmount;
        this.PoisonAmount = PoisonAmount;
        this.LifestealAmount = LifestealAmount;

        _arrayId = arrayId;
    }

    public int GetArrayId()
    {
        return _arrayId;
    }

    public void Poison(bool affectLeftPlayer)
    {
        if (PoisonAmount > 0)
        {
            PoisonAttackLogic.Instance.AddPoisonToPlayer(affectLeftPlayer, PoisonAmount);
        }
    }
    public void Heal(bool affectLeftPlayer)
    {
        if (HealAmount > 0)
        {
            PlayersHealthBars.Instance.IncreaseHealthValue(affectLeftPlayer, HealAmount);
        }
    }
    public void Attack(bool affectLeftPlayer)
    {
        if (AttackAmount > 0)
        {
            PlayersHealthBars.Instance.DecreaseHealthValue(affectLeftPlayer, AttackAmount);
        }
    }
    public void Lifesteal(bool leftPlayerIsAttacked)
    {
        if (LifestealAmount > 0)
        {
            PlayersHealthBars.Instance.DecreaseHealthValue(leftPlayerIsAttacked, LifestealAmount);
            PlayersHealthBars.Instance.IncreaseHealthValue(!leftPlayerIsAttacked, LifestealAmount);
        }
    }
}
