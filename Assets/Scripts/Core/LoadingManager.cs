using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            string sceneName = SceneManager.GetActiveScene().name == Constants.Levels.Level1 ? Constants.Levels.Level2 : Constants.Levels.Level1;
            SceneManager.LoadScene(sceneName);
        }
    }
}
