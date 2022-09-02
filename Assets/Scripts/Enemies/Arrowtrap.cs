using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowtrap : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject[] _arrows;
    private float _cooldownTimer;
    private int _arrowCounter = 0;

    private void Awake()
    {
        _cooldownTimer = _attackCooldown;
    }

    private void Attack()
    {
        _cooldownTimer = 0;
        GameObject arrow = _arrows[_arrowCounter];
        arrow.transform.position = _firePoint.position;
        arrow.GetComponent<EnemyProjectile>().ActivateProjectile();
        _arrowCounter = _arrowCounter == _arrows.Length ? _arrowCounter = 0 : _arrowCounter + 1;
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        GameHandler.WriteDebug(_cooldownTimer.ToString());
        if (_cooldownTimer >= _attackCooldown)
        {
            Attack();
        }
    }
}
