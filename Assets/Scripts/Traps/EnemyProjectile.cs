using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float _speed;
    [SerializeField] private float _resetTime;
    private float _lifetime;

    private void Update()
    {
        float movementSpeed = _speed * Time.deltaTime;
        transform.Translate(new Vector3(movementSpeed, 0, 0));
        _lifetime += Time.deltaTime;
        if(_lifetime > _resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void ActivateProjectile()
    {
        _lifetime = 0;
        gameObject.SetActive(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}
