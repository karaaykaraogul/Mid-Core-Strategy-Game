using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitFactoryStatic;
using UnityEngine.UI;
using BuildingFactoryStatic;

public class UnitButton : MonoBehaviour
{
    GameObject initializedFromBuilding;
    [SerializeField] Text buttonText;

    public void Init(GameObject initializedFromBuilding)
    {
        this.initializedFromBuilding = initializedFromBuilding;
    }

    public void SetUnitName(string name)
    {
        buttonText.text = name;
        this.enabled = true;
    }

    public void BuyUnit()
    {
        if(PlayerResourceManager.instance.GetCurrentUnitAmount() < PlayerResourceManager.instance.GetUnitCap())
        {
            var unit = UnitFactory.GetUnit(buttonText.text);
            Debug.Log("clicked on: " + unit.Name);
            unit.Process(initializedFromBuilding);
        }
    }
}
