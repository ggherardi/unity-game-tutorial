using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] _options;
    [SerializeField] private AudioClip _changeSound;
    [SerializeField] private AudioClip _interactSound;
    private RectTransform _rect;
    private int _currentPosition;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Change position of the arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        // Interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    private void ChangePosition(int change)
    {
        _currentPosition += change;
        if(change != 0)
        {
            SoundManager.PlaySound(_changeSound);
        }
        if (_currentPosition < 0)
        {
            _currentPosition = _options.Length - 1;
        }
        else if (_currentPosition > _options.Length - 1)
        {
            _currentPosition = 0;
        }

        // Assign the Y position of of the arrow
        _rect.position = new Vector3(_rect.position.x, _options[_currentPosition].position.y, _rect.position.x);
    }

    private void Interact()
    {
        SoundManager.PlaySound(_interactSound);

        _options[_currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
