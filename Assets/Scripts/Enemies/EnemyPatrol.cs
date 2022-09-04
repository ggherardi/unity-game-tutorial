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
    private Vector3 _initialScale;
    private bool _movingLeft;

    private void Awake()
    {
        _movingLeft = Mathf.Sign(_enemy.localScale.x) == Constants.Directions.Left;
        _initialScale = _enemy.localScale;
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (_enemy.position.x <= _leftEdge.position.x)
            {
                MoveInDirection(Constants.Directions.Right);
                _movingLeft = false;
            }
            else
            {
                MoveInDirection(Constants.Directions.Left);
            }
        }
        else
        {
            if(_enemy.position.x >= _rightEdge.position.x)
            {
                MoveInDirection(Constants.Directions.Left);
                _movingLeft = true;
            }
            else
            {
                MoveInDirection(Constants.Directions.Right);
            }
        }
    }

    private void MoveInDirection(int direction)
    {
        // Make enemy face direction
        _enemy.localScale = new Vector3(Mathf.Abs(_initialScale.x) * direction, _enemy.localScale.y, _enemy.localScale.z);

        // Move in that direction
        _enemy.position = new Vector3(_enemy.position.x + Time.deltaTime * direction * _speed, _enemy.position.y, _enemy.position.z);
    }
}
