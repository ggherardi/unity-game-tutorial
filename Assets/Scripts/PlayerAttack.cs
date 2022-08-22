using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float _cooldownTimer = Mathf.Infinity;
    [SerializeField]
    private float _attackCooldown;
    private Animator _playerAnimator;
    private PlayerMovement _playerMovementHandler;

    public void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMovementHandler = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (GameHandler.HasRightClicked && _attackCooldown < _cooldownTimer && _playerMovementHandler.CanAttack())
        {
            Attack();
        }

        _cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        _playerAnimator.SetTrigger(Constants.Animations.AttackTrigger);
        _cooldownTimer = 0;
    }
}
