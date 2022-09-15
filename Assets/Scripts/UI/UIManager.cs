using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private AudioClip _gameOverSound;

    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
        SoundManager.PlaySound(_gameOverSound);
    }
}
