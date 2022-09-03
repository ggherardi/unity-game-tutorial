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
                _camera.GetComponent<RoomCameraHandler>().MoveToNewRoom(_nextRoom);
                _nextRoom.GetComponent<Room>().ActivateRoom(true);
                _previousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                _camera.GetComponent<RoomCameraHandler>().MoveToNewRoom(_previousRoom);
                _nextRoom.GetComponent<Room>().ActivateRoom(false);
                _previousRoom.GetComponent<Room>().ActivateRoom(true);
            }
        }
    }
}
