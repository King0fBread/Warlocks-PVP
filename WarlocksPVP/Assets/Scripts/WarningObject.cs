using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningObject : MonoBehaviour
{
    [SerializeField] private float _timeBeforeDisabling;
    private float _currentTime;
    private void OnEnable()
    {
        _currentTime = _timeBeforeDisabling;
    }
    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
            gameObject.SetActive(false);
    }
}
