using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float _speed;
    [SerializeField] private float _resetTime;
    private float _direction;
    private Animator _animator;
    private Collider2D _collider;
    private float _lifetime;
    private bool _hit;

    private void Awake()
    {
        _animator = GetComponent<Animator>(); 
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!_hit)
        {
            float movementSpeed = _speed * Time.deltaTime;
            transform.Translate(new Vector3(movementSpeed * _direction, 0, 0));
            _lifetime += Time.deltaTime;
            if (_lifetime > _resetTime)
            {
                gameObject.SetActive(false);
            }
        }        
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
        transform.localScale = new Vector3(direction * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void ActivateProjectile()
    {
        _lifetime = 0;        
        gameObject.SetActive(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.tag == Constants.Tags.Player || collision.tag == Constants.Tags.Walls)
        {
            if(_collider != null)
            {
                _collider.enabled = false;
            }            
            _animator.SetTrigger(Constants.Animations.Fireball.ExplodeTrigger);
            _hit = true;
        }        
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
