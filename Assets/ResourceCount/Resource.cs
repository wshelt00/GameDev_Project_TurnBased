using UnityEngine;

public class Resource : MonoBehaviour
{

    public string type;
    public int quantity = 3;
    public int goldQunat = 500;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            if(type == "Gold")
            {

                ResourceManager.resource.addResources(type, goldQunat);
                Destroy(gameObject);

            } else
            {

                ResourceManager.resource.addResources(type, quantity);
                Destroy(gameObject);

            }
            

        }

    }

}
