using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform _enemy;

    [Header("Movement parameters")]
    [SerializeField] private float _speed;

    [Header("Idle Behaviour")]
    [SerializeField] private float _idleTime;
    private float _idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator _enemyAnimator;

    private Vector3 _initialScale;
    private bool _movingLeft;

    private void Awake()
    {
        _movingLeft = Mathf.Sign(_enemy.localScale.x) == Constants.Directions.Left;
        _initialScale = _enemy.localScale;
        _idleTimer = _idleTime;
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (_enemy.position.x >= _leftEdge.position.x)
            {
                MoveInDirection(Constants.Directions.Left);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (_enemy.position.x <= _rightEdge.position.x)
            {
                MoveInDirection(Constants.Directions.Right);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void OnDisable()
    {
        _enemyAnimator.SetBool(Constants.Animations.Bandit.IsRunning, false);
    }

    private void DirectionChange()
    {
        _enemyAnimator.SetBool(Constants.Animations.Bandit.IsRunning, false);
        _idleTimer += Time.deltaTime;
        if(_idleTimer > _idleTime)
        {
            _movingLeft = !_movingLeft;
        }
    }

    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;
        _enemyAnimator.SetBool(Constants.Animations.Bandit.IsRunning, true);

        // Make enemy face direction
        _enemy.localScale = new Vector3(Mathf.Abs(_initialScale.x) * direction, _enemy.localScale.y, _enemy.localScale.z);

        // Move in that direction
        _enemy.position = new Vector3(_enemy.position.x + Time.deltaTime * direction * _speed, _enemy.position.y, _enemy.position.z);
    }
}
