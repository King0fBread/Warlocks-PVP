using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Card : INetworkSerializable
{
    public Sprite CardSprite;

    public int CardSpriteId;
    public string CardName;
    public int AttackAmount;
    public int HealAmount;
    public int PoisonAmount;
    public int LifestealAmount;

    public Card(Sprite CardSprite, int CardSpriteId, string CardName, int AttackAmount, int HealAmount, int PoisonAmount, int LifestealAmount)
    {
        this.CardSprite = CardSprite;
        this.CardSpriteId = CardSpriteId;
        this.CardName = CardName;
        this.AttackAmount = AttackAmount;
        this.HealAmount = HealAmount;
        this.PoisonAmount = PoisonAmount;
        this.LifestealAmount = LifestealAmount;
    }

    public int ExecutionIndex;

    public void ExecuteAttack()
    {

    }
    private void Poison()
    {
        if(PoisonAmount > 0)
        {
            Debug.Log("poison");
            //apply poison to opponent on next turn

            //NOT THE 'APPLIED EARILER' POISON LOGIC
        }
    }
    private void Heal()
    {
        if(HealAmount > 0)
        {
            Debug.Log("heal");
            //applay heal to owner on call
        }
    }
    private void Attack()
    {
        if(AttackAmount > 0)
        {
            Debug.Log("attack");
            //apply attack to oppenent on call
        }
    }
    private void Lifesteal()
    {
        if(LifestealAmount > 0)
        {
            Debug.Log("lifesteal");
            //apply damage to oppenent and heal owner on call
        }
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref CardSpriteId);
        serializer.SerializeValue(ref CardName);
        serializer.SerializeValue(ref AttackAmount);
        serializer.SerializeValue(ref HealAmount);
        serializer.SerializeValue(ref PoisonAmount);
        serializer.SerializeValue(ref LifestealAmount);
    }
}
