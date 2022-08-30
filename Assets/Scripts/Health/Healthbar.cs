using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Image _totalHealthbar;
    [SerializeField] private Image _currentHealthbar;

    private void Start()
    {
        _totalHealthbar.fillAmount = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
    }

    private void Update()
    {
        _currentHealthbar.fillAmount = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
    }
}
