using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _cooldownTimer = Mathf.Infinity;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private Transform _firePoint; // Position from which the bullets will be fired
    [SerializeField] private GameObject[] _fireballs;
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

    // Object pooling will be used instead of instantiate and destroy
    private void Attack()
    {
        _playerAnimator.SetTrigger(Constants.Animations.AttackTrigger);
        _cooldownTimer = 0;

        // Moving one of the fireballs to the firepoint
        _fireballs[0].transform.position = _firePoint.position;
        _fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
