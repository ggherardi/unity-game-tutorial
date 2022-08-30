using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth { get; private set; }
    private Animator _playerAnimator;
    private bool _dead;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        CurrentHealth = _maxHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButton(2))
        {
            TakeDamage(0.1f);
        }
    }

    public void TakeDamage(float damage)
    {
        // Returns min if value < min, value if min < value < max, max if value < value
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _maxHealth);
        if(CurrentHealth > 0)
        {
            _playerAnimator.SetTrigger(Constants.Animations.HurtTrigger);
            //iframes
        }
        else
        {
            if (!_dead)
            {
                _playerAnimator.SetTrigger(Constants.Animations.DeathTrigger);
                GetComponent<PlayerMovement>().enabled = false;
                _dead = true;
            }            
        }
    }
}
