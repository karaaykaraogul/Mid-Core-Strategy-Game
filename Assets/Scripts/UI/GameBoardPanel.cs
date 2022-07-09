using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingFactoryStatic;
using UnitFactoryStatic;

public class GameBoardPanel : MonoBehaviour
{
    void Start()
    {
        GameEvents.current.onEntityCreated += OnEntityCreated;
    }

    private void OnEntityCreated()
    {
        Debug.Log("entity created");
        var buildings = FindObjectsOfType<Building>();
        foreach(var building in buildings)
        {
            Debug.Log(building.name);
        }

        var units = FindObjectsOfType<Unit>();
        foreach(var unit in units)
        {
            Debug.Log(unit.name);
        }
    }
}
