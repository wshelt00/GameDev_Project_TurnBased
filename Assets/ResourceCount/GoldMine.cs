using UnityEngine;

public class GoldMine : MonoBehaviour
{

    private bool isCaptured = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player") && isCaptured == false)
        {

            isCaptured = true;
            ResourceManager.resource.capMine();
           
        }

    }

}
