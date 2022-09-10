using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float _healAmountPercentage;

    [Header("SFX")]
    [SerializeField] private AudioClip _healSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constants.Tags.Player)
        {
            collision.GetComponent<Health>().HealByPercentage(_healAmountPercentage);
            SoundManager.PlaySound(_healSound);
            gameObject.SetActive(false);
        }
    }
}
