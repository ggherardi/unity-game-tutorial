using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private float _direction;
    private bool _hit;
    private BoxCollider2D _projectileCollider;
    private Animator _projectileAnimator;

    private void Awake()
    {
        _projectileCollider = GetComponent<BoxCollider2D>();
        _projectileAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_hit) return;
        float _movementSpeed = Speed * Time.deltaTime * _direction;
        transform.Translate(new Vector3(_movementSpeed, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;
        _projectileCollider.enabled = false;
        _projectileAnimator.SetTrigger(Constants.Animations.ExplodeTrigger);
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
        gameObject.SetActive(true);
        _hit = false;
        _projectileCollider.enabled = true;

        // Check if direction is correct and in case flip
        float transformX = transform.localScale.x;
        if(Mathf.Sign(transformX) != direction)
        {
            transformX = -transformX;
        }
        transform.localScale = new Vector3(transformX, transform.localScale.y, transform.localScale.z);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
