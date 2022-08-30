using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float _healAmountPercentage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constants.Tags.Player)
        {
            collision.GetComponent<Health>().HealByPercentage(_healAmountPercentage);
            gameObject.SetActive(false);
        }
    }
}
