using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoisonAttackLogic : MonoBehaviour
{
    public static PoisonAttackLogic Instance;

    [SerializeField] private SpriteRenderer _leftPlayerPoisonIcon;
    [SerializeField] private SpriteRenderer _rightPlayerPoisonIcon;

    [SerializeField] private TextMeshProUGUI _leftAmountText;
    [SerializeField] private TextMeshProUGUI _rightAmountText;

    private int _currentLeftPoisonAmount;
    private int _currentRightPosionAmount;
    private void Awake()
    {
        Instance = this;

        _leftPlayerPoisonIcon.gameObject.SetActive(false);
        _rightPlayerPoisonIcon.gameObject.SetActive(false);
    }
    public void AddPoisonToPlayer(bool affectLeftPlayer, int poisonAmount)
    {
        if (affectLeftPlayer)
        {
            _leftPlayerPoisonIcon.gameObject.SetActive(true);
            int previousAmount = int.Parse(_leftAmountText.text);
            _leftAmountText.text = (previousAmount + poisonAmount).ToString();
        }
        else
        {
            _rightPlayerPoisonIcon.gameObject.SetActive(true);
            int previousAmount = int.Parse(_rightAmountText.text);
            _rightAmountText.text = (previousAmount + poisonAmount).ToString();
        }
    }
    public void ApplyExistingPoison()
    {
        if(_currentLeftPoisonAmount > 0)
        {
            //decrease health
            _leftAmountText.text = "0";
            _leftPlayerPoisonIcon.gameObject.SetActive(false);
        }
        else if(_currentRightPosionAmount > 0)
        {
            //decrease health
            _rightAmountText.text = "0";
            _rightPlayerPoisonIcon.gameObject.SetActive(false);
        }
    }
    
}
