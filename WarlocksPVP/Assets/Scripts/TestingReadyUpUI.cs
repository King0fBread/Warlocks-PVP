using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingReadyUpUI : MonoBehaviour
{
    [SerializeField] private Button _readyButton;
    private void Awake()
    {
        _readyButton.onClick.AddListener(() => PlayerReadyUp.Instance.SetPlayerAsReady());
    }
}
