using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Card : NetworkBehaviour
{
    private Sprite CardSprite;
    private string CardName;
    private int AttackAmount;
    private int HealAmount;
    private int PoisonAmount;
    private int LifestealAmount;

    public Card(Sprite CardSprite, string CardName, int AttackAmount, int HealAmount, int PoisonAmount, int LifestealAmount)
    {
        this.CardSprite = CardSprite;
        this.CardName = CardName;
        this.AttackAmount = AttackAmount;
        this.HealAmount = HealAmount;
        this.PoisonAmount = PoisonAmount;
        this.LifestealAmount = LifestealAmount;
    }

    public int ExecutionIndex;

    public Sprite GetCardSprite()
    {
        return CardSprite;
    }
    public void ExecuteAttack()
    {

    }
    private void Posion()
    {
        if(PoisonAmount > 0)
        {
            print("poison");
            //apply poison to opponent on next turn

            //NOT THE 'APPLIED EARILER' POISON LOGIC
        }
    }
    private void Heal()
    {
        if(HealAmount > 0)
        {
            print("heal");
            //applay heal to owner on call
        }
    }
    private void Attack()
    {
        if(AttackAmount > 0)
        {
            print("attack");
            //apply attack to oppenent on call
        }
    }
    private void Lifesteal()
    {
        if(LifestealAmount > 0)
        {
            print("lifesteal");
            //apply damage to oppenent and heal owner on call
        }
    }
}
