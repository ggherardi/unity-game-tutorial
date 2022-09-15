using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip _checkpointSound;
    private Transform _checkpointTransform;
    private Health _playerHealth;
    private UIManager _uiManager;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        // Check if check point is available
        if(_checkpointTransform == null)
        {
            // Game over
            _uiManager.GameOver();
            return;
        }

        // Move player to checkpoint 
        transform.position = _checkpointTransform.position;
        _playerHealth.HealthRespawn();

        // Move camera to checkpoint room (for this to work the checkpoint objects has to be placed as child of the room object)
        Camera.main.GetComponent<RoomCameraHandler>().MoveToNewRoom(_checkpointTransform.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == Constants.Tags.Checkpoint)
        {            
            _checkpointTransform = collision.transform; // Store the checkpoint transform
            SoundManager.PlaySound(_checkpointSound);            
            collision.GetComponent<Collider2D>().enabled = false; // Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger(Constants.Animations.Checkpoint.AppearTrigger);
        }
    }
}
