using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCameraHandler : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _currentPosX;
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        // It multiplies Time.DeltaTime to make it frame rate indipendent
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_currentPosX, transform.position.y, transform.position.z), ref _velocity, _speed);
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        _currentPosX = newRoom.position.x;
    }
}
