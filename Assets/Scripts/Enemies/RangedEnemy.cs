using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _range;

    [Header("Collider Parameters")]
    [SerializeField] private float _colliderDistance;
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Ranged Attack")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject[] _fireballs;
    private int _fireballCounter = 0;

    [Header("Player Layer")]
    [SerializeField] private LayerMask _playerLayerMask;

    private float _cooldownTimer = Mathf.Infinity;
    private bool _isAttacking = false;

    // References
    private Animator _animator;
    private EnemyPatrol _enemyPatrol;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (IsPlayerInSight())
        {
            if (_cooldownTimer > _attackCooldown)
            {
                _cooldownTimer = 0;
                _animator.SetTrigger(Constants.Animations.Generics.RangedAttack);
                _isAttacking = true;
            }
        }

        if (_enemyPatrol != null)
        {
            // This stops the bandit if the player enters in sight by disabling the patrolling script
            _enemyPatrol.enabled = !IsPlayerInSight();
        }
    }

    private bool IsPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z),
            0, Vector2.left, 0,
            _playerLayerMask);

        return hit.collider != null;
    }

    private void RangedAttack()
    {
        _cooldownTimer = 0;

        EnemyProjectile projectile = _fireballs[_fireballCounter].GetComponent<EnemyProjectile>();
        _fireballs[_fireballCounter].transform.position = _firePoint.position;
        projectile.SetDirection(Mathf.Sign(transform.localScale.x));
        projectile.ActivateProjectile();

        // FindFireballIndex: The method I made is a bit different, don't know if it's more efficient. This way I just increment the index of the fireball to use next attack, without loops.
        _fireballCounter = _fireballCounter == _fireballs.Length - 1 ? 0 : _fireballCounter + 1;
    }

    private void EndAttack()
    {
        _isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));
    }
}
