using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReusedCardWarning : MonoBehaviour
{
    private float _timeBeforeDisabling;
    private void OnEnable()
    {
        _timeBeforeDisabling = 1.5f;
    }
    private void Update()
    {
        _timeBeforeDisabling -= Time.deltaTime;
        if (_timeBeforeDisabling <= 0)
            gameObject.SetActive(false);
    }
}
