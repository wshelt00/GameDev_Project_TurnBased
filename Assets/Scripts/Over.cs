using UnityEngine;
using UnityEngine.SceneManagement;


public class Over : MonoBehaviour
{

    public void loadMenu()
    {

        SceneManager.LoadScene("Menu");

    }

    public void Exit()
    {

        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();

    }

}
