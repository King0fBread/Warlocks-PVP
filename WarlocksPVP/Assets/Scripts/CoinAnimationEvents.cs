using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimationEvents : MonoBehaviour
{
    public void Hide()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
