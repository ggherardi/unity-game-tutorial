using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    [SerializeField] private float _activationDelay;
    [SerializeField] private float _activeTime;
    [SerializeField] private float _damage;
    private Animator _firetrapAnimator;
    private SpriteRenderer _firetrapSpriteRenderer;

    [Header("Sound")]
    [SerializeField] private AudioClip _burnSound;

    private bool _triggered;
    private bool _active;

    private void Awake()
    {
        _firetrapAnimator = gameObject.GetComponent<Animator>();
        _firetrapSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
    }

    private IEnumerator ActivateTrap()
    {
        _triggered = true;
        _firetrapAnimator.SetTrigger(Constants.Animations.Firetrap.IsHit);

        yield return new WaitForSeconds(_activationDelay);
        _active = true;
        _firetrapAnimator.SetBool(Constants.Animations.Firetrap.Active, true);
        SoundManager.PlaySound(_burnSound);

        yield return new WaitForSeconds(_activeTime);
        _active = false;
        _triggered = false;
        _firetrapAnimator.SetBool(Constants.Animations.Firetrap.Active, false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_active)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.Tags.Player)
        {
            if (!_triggered)
            {
                StartCoroutine(ActivateTrap());
            }
            if (_active)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
            }
        }
    }
}
