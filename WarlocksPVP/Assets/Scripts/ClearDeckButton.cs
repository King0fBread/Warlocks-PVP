using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearDeckButton : MonoBehaviour
{
    private Button _cancelButton;
    private void Awake()
    {
        _cancelButton = GetComponent<Button>();
        _cancelButton.onClick.AddListener(() => { CardSlotsAssigner.Instance.ClearHolders(); });
    }
}
