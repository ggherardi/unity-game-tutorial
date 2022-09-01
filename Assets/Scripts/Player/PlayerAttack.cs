using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _cooldownTimer = Mathf.Infinity;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private Transform _firePoint; // Position from which the fireballs will be fired
    [SerializeField] private GameObject[] _fireballsHolder;
    private int _fireballCounter = 0;
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
        _playerAnimator.SetTrigger(Constants.Animations.Player.AttackTrigger);
        _cooldownTimer = 0;

        // Moving one of the fireballs to the firepoint
        _fireballsHolder[_fireballCounter].transform.position = _firePoint.position;
        _fireballsHolder[_fireballCounter].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        // FindFireballIndex: The method I made is a bit different, don't know if it's more efficient. This way I just increment the index of the fireball to use next attack, without loops.
        _fireballCounter = _fireballCounter == _fireballsHolder.Length - 1 ? 0 : _fireballCounter + 1;
    }

    // FindFireballIndex: Method made by the tutorial
    private int FindFireball()
    {
        for(int i = 0; i < _fireballsHolder.Length; i++)
        {
            if (!_fireballsHolder[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
