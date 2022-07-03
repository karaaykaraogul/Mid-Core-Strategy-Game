using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitFactoryStatic;

public class UnitClient : Singleton<UnitClient>
{
    public void InitializeUnit(GameObject unit, Unit buildingClass)
    {
        Debug.Log("initalizing: " + unit.name);
        //initializing a unit function will be here
    }
}