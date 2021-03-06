using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuildingFactoryStatic;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] Text buttonText;
    public void SetBuildingName(string name)
    {
        buttonText.text = name;
        this.enabled = true;
    }

    public void BuyBuilding()
    {
        if(PlayerResourceManager.instance.GetCurrentBuildingAmount() < PlayerResourceManager.instance.GetBuildingCap())
        {
            var building = BuildingFactory.GetBuilding(buttonText.text);
            building.Process();
        }
    }
}
