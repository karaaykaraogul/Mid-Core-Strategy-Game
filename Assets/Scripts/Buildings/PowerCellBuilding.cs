using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingFactoryStatic;

public class PowerCellBuilding : Building, IBuilding, ICapBuilding
{
    public override string Name => "Power Cell";
    public override string PrefabName => "PowerCellPrefab";
    public override int Width => 1;
    public override int Height => 1;
    
    public string buildingName 
        { 
            get => this.Name;
        }

    public int capIncreaseAmount => 100;

    public int buildingCost => 10;

    void OnEnable()
    {
        PlayerResourceManager.instance.IncreasePowerCap(capIncreaseAmount);
    }

    void OnDestroy()
    {
        PlayerResourceManager.instance.DecreasePowerCap(capIncreaseAmount);
    }
}
