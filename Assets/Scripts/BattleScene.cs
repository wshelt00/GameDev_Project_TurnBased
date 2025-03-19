using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {

            SceneManager.LoadScene("BattleScene");

        }

    }

}
