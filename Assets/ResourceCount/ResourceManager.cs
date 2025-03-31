using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    
    public static ResourceManager resource;

    public int gold = 0;
    public int wood = 0;
    public int stone = 0;
    public int crystal = 0;
    public int numMines = 0;
    int goldTotal = 0;

    public Text goldText;
    public Text woodText;
    public Text stoneText;
    public Text crystalText;

    private void Awake()
    {
        
        if(resource == null)
        {

            resource = this;

        }

    }

    public void addResources(string type, int quantity)
    {

        if(type == "Gold")
        {

            gold += quantity;
            goldText.text = gold.ToString();

        } else if(type == "Wood")
        {

            wood += quantity;
            woodText.text = wood.ToString();

        } else if(type == "Stone")
        {

            stone += quantity;
            stoneText.text = stone.ToString();

        } else if(type == "Crystal")
        {

            crystal += quantity;
            crystalText.text = crystal.ToString();

        }

    }

    public void goldMine()
    {

        if(numMines > 0)
        {

            goldTotal = 500 * numMines;
            gold += goldTotal;
            goldText.text = gold.ToString();

        }

    }

    public void capMine()
    {

        numMines++;

    }

    public void loseMine()
    {

        if(numMines > 0)
        {

            numMines--;

        }

    }

    public void goldUpdate()
    {

        goldText.text = gold.ToString();

    }

}
