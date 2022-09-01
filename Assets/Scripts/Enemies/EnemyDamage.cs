using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constants.Tags.Player)
        {
            GameHandler.WriteDebug("Ciao");
            collision.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}
