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
        Debug.Log("Exited");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
