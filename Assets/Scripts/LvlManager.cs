using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string scene;

    public void changeScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void exitScene()
    {   

        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();

    }
}
