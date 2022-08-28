using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _previousRoom;
    [SerializeField] private Transform _nextRoom;
    [SerializeField] private RoomCameraHandler _camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constants.GameObjects.Player)
        {
            if(collision.transform.position.x < transform.position.x)
            {
                GameHandler.Camera.CameraHandler.MoveToNewRoom(_nextRoom);
            }
            else
            {
                GameHandler.Camera.CameraHandler.MoveToNewRoom(_previousRoom);
            }
        }
    }
}
