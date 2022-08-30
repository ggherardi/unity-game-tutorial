using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _movementDistance;
    [SerializeField] private float _speed;
    private bool _movingLeft;
    private float _leftEdge;
    private float _rightEdge;

    private void Awake()
    {
        _leftEdge = transform.position.x - _movementDistance;
        _rightEdge = transform.position.x + _movementDistance;
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if(transform.position.x > _leftEdge)
            {
                transform.position = new Vector3(transform.position.x - _speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                _movingLeft = false;
            }    
        }
        else
        {
            if (transform.position.x < _rightEdge)
            {
                transform.position = new Vector3(transform.position.x + _speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                _movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constants.Tags.Player)
        {
            collision.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}
