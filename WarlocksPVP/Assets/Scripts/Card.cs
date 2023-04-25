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
    public void Heal()
    {
        if (HealAmount > 0)
        {
            Debug.Log("heal");
            //applay heal to owner on call
        }
    }
    public void Attack()
    {
        if (AttackAmount > 0)
        {
            Debug.Log("attack");
            //apply attack to oppenent on call
        }
    }
    public void Lifesteal()
    {
        if (LifestealAmount > 0)
        {
            Debug.Log("lifesteal");
            //apply damage to oppenent and heal owner on call
        }
    }
}
