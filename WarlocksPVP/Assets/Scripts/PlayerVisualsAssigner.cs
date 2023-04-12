using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsAssigner : MonoBehaviour
{
    [SerializeField] private Transform[] _playerPositions;
    [SerializeField] private Sprite[] _playerSprites;

    public static PlayerVisualsAssigner Instance;

    private void Awake()
    {
        Instance = this;
    }
    public Transform GetTransformByID(int transformID)
    {
        return _playerPositions[transformID];
    }
    public Sprite GetSpriteByID(int spriteID)
    {
        return _playerSprites[spriteID];
    }
}
