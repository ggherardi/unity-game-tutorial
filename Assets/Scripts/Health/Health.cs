using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth { get; private set; }
    private Animator _playerAnimator;
    private bool _dead;

    [Header("iFrames")]
    [SerializeField] private float _iframesDuration;
    [SerializeField] private int _numberOfFlashes;
    private SpriteRenderer _playerSpriteRenderer;

    private void Awake()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
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

    private IEnumerator IFrame()
    {
        Physics2D.IgnoreLayerCollision(Constants.Layers.Player, Constants.Layers.Enemy, true);
        for(int i = 0; i < _numberOfFlashes; i++)
        {
            _playerSpriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(_iframesDuration / _numberOfFlashes / 2); // or (_iframesDuration / (_numberOfFlashes * 2))
            _playerSpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(_iframesDuration / _numberOfFlashes / 2); // or (_iframesDuration / (_numberOfFlashes * 2))
        }        
        Physics2D.IgnoreLayerCollision(Constants.Layers.Player, Constants.Layers.Enemy, false);
    }

    public void TakeDamage(float damage)
    {
        // Returns min if value < min, value if min < value < max, max if value < value
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _maxHealth);
        if(CurrentHealth > 0)
        {            
            _playerAnimator.SetTrigger(Constants.Animations.Player.HurtTrigger);
            StartCoroutine(IFrame());
            //iframes
        }
        else
        {
            if (!_dead)
            {
                _playerAnimator.SetTrigger(Constants.Animations.Player.DeathTrigger);
                GetComponent<PlayerMovement>().enabled = false;
                _dead = true;
            }            
        }
    }    

    public void HealByPercentage(float healAmountPercentage)
    {
        float healAmount = MaxHealth * healAmountPercentage;
        CurrentHealth = Mathf.Clamp(CurrentHealth + healAmount, CurrentHealth, MaxHealth);
    }
}
