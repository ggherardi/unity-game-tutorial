using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Base melee stats")]
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _range;
    [SerializeField] private float _colliderDistance;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private BoxCollider2D _boxCollider;
    private float _cooldownTimer = Mathf.Infinity;

    // References
    private Animator _animator;
    private Health _playerHealth;
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
                _animator.SetTrigger(Constants.Animations.Generics.MeleeAttack);
            }
        }        

        if(_enemyPatrol != null)
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
        if( hit.collider != null)
        {
            _playerHealth = hit.collider.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        if (IsPlayerInSight())
        {
            _playerHealth.TakeDamage(_attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance, 
            new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));
    }
}
