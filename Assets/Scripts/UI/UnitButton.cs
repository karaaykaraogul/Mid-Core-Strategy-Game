using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitFactoryStatic;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] Text buttonText;
    public void SetUnitName(string name)
    {
        buttonText.text = name;
        this.enabled = true;
    }

    public void BuyUnit()
    {
        var unit = UnitFactory.GetUnit(buttonText.text);
        Debug.Log("clicked on: " + unit.Name);
        unit.Process();
    }
}
