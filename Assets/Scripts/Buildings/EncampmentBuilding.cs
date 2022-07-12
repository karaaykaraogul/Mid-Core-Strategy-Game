using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingFactoryStatic;

public class EncampmentBuilding : Building, IBuilding, ICapBuilding
{
    public override string Name => "Encapment";
    public override string PrefabName => "EncampmentPrefab";
    public override int Width => 2;
    public override int Height => 2;
    
    public string buildingName 
        { 
            get => this.Name;
        }

    public int capIncreaseAmount => 5;

    public int buildingCost => 10;

    void OnEnable()
    {
        PlayerResourceManager.instance.IncreaseSoldierCap(capIncreaseAmount);
    }

    void OnDestroy()
    {
        PlayerResourceManager.instance.DecreaseSoldierCap(capIncreaseAmount);
    }
}
