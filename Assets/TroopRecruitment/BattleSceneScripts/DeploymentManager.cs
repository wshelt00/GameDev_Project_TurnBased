using System.Collections.Generic;
using NUnit.Framework;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class DeploymentManager : MonoBehaviour
{

    public List<Image> equipImages;

    void Start()
    {

       loadUnits();

    }

    public void loadUnits() // here
    {

        var equipUnits = TroopStorage.tps.equStats;

        for(int i = 0; i < equipImages.Count; i++)
        {

            if(i < equipUnits.Count && equipUnits[i].icon != null)
            {

                equipImages[i].sprite = equipUnits[i].icon;
                equipImages[i].enabled = true;

                RectTransform rt = equipImages[i].rectTransform;

                if (equipUnits[i].unitName == "Champion")
                {

                    rt.sizeDelta = new Vector2(50, 50);

                }

            }

        }

    }
   

}
