using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float Damage;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constants.Tags.Player)
        {
            collision.GetComponent<Health>().TakeDamage(Damage);
        }
    }
}
