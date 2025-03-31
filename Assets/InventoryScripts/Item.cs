using UnityEngine;

public class Item : MonoBehaviour
{

    public string itemName;
    public Sprite icon;
    public string arti;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {

            if(InventoryManager.inv.addItem(this)) 
            {

                gameObject.SetActive(false);

            }

        }

    }

}
