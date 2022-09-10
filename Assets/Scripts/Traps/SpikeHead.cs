using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private float _attackDelay;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _wallLayer;
    private float _checkTimer;
    private Vector3 _destination;    
    private bool _attacking;

    [Header("SFX")]
    [SerializeField] private AudioClip _smashSound;

    private Vector3[] _directions = new Vector3[4];

    private void Update()
    {
        if (_attacking)
        {
            transform.Translate(_destination * Time.deltaTime * _speed);
        }
        else
        {
            _checkTimer += Time.deltaTime;
            if(_checkTimer > _attackDelay)
            {
                CheckForPlayer();
            }

        }
    }

    private void OnEnable()
    {
        Stop();
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        for(int i = 0; i < _directions.Length; i++)
        {
            Debug.DrawRay(transform.position, _directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _directions[i], _range, _playerLayer); // Vector3D can fit in Vector2D
            if (hit.collider != null && !_attacking)
            {
                _attacking = true;
                _destination = _directions[i];
                _checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {        
        _directions[0] = transform.right * _range;
        _directions[1] = -transform.right * _range;
        _directions[2] = transform.up * _range;
        _directions[3] = -transform.up * _range;
    }

    private void Stop()
    {
        _destination = transform.position;
        _attacking = false;
        _checkTimer = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {        
        base.OnTriggerEnter2D(collision);
        if(collision.tag != Constants.Tags.PlayerFireball)
        {
            SoundManager.PlaySound(_smashSound);
            Stop();
        }
    }
}
