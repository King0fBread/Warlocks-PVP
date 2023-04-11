using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsAssigner : MonoBehaviour
{
    [SerializeField] private Transform[] _playerPositions;
    [SerializeField] private Sprite[] _playerSprites;
    private Transform _clientPlayerPosition;
    private int _clientPlayerSpriteID;

    public static PlayerVisualsAssigner Instance;

    private void Awake()
    {
        Instance = this;
    }
    public Transform GetRandomPositionOnHost()
    {
        int randomPositionID = Random.Range(0, 2);
        _clientPlayerPosition = _playerPositions[randomPositionID == 0 ?  1 :  0];
        return _playerPositions[randomPositionID];
    }
    public Transform GetRandomPositionOnClient()
    {
        return _clientPlayerPosition;
    }
    public Sprite GetRandomSpriteOnHost()
    {
        int randomSpriteID = Random.Range(0, 2);
        _clientPlayerSpriteID = randomSpriteID == 0 ? 1 : 0;
        return _playerSprites[randomSpriteID];
    }
    public int GetRandomSpriteIDOnClient()
    {
        return _clientPlayerSpriteID;
    }
    public Sprite GetSpriteByID(int spriteID)
    {
        return _playerSprites[spriteID];
    }
}
