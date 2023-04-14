using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
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
    public void Attack()
    {

    }
}
