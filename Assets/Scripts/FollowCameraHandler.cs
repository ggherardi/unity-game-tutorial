using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraHandler : MonoBehaviour
{
    [SerializeField] private float _aheadDistance;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _playerTransform;
    private Vector3 _velocity;
    private float _lookAhead;

    private void Update()
    {
        // Using the singleton to retrieve useer
        //transform.position = new Vector3(GameHandler.Player.GameObject.transform.position.x, GameHandler.Player.GameObject.transform.position.y, transform.position.z);

        // Using the script parameter
        GameHandler.WriteDebug($"_lookAhead: {_lookAhead}{Environment.NewLine}Time.deltaTime: {Time.deltaTime}", false);
        transform.position = new Vector3(_playerTransform.position.x + _lookAhead, _playerTransform.position.y, transform.position.z);
        float cameraDirection = GameHandler.HorizontalInput == 0 ? 0 : _playerTransform.localScale.x;
        _lookAhead = Mathf.Lerp(_lookAhead, cameraDirection, _speed * Time.deltaTime);        
    }
}
