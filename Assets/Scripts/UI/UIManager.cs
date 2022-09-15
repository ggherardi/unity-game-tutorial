using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private AudioClip _gameOverSound;

    private void Awake()
    {
        _gameOverScreen.SetActive(false);
    }

    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
        SoundManager.PlaySound(_gameOverSound);
    }

    // Menu functions
    public void Restart()
    {
        // Restart current level (GetActiveScene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // Usualle the first scene is the main menu
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
