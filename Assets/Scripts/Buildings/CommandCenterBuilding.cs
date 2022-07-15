using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingFactoryStatic;

public class CommandCenterBuilding : Building, IBuilding, ICapBuilding
{
    public override string Name => "Command Center";
    public override string PrefabName => "CommandCenterPrefab";
    public override int Width => 3;
    public override int Height => 2;
    
    public string buildingName 
        { 
            get => this.Name;
        }

    public int capIncreaseAmount => 5;

    public int buildingCost => 50;

    void OnEnable()
    {
        PlayerResourceManager.instance.IncreaseBuildingCap(capIncreaseAmount);
    }

    public void DecreaseCap()
    {
        PlayerResourceManager.instance.DecreaseBuildingCap(capIncreaseAmount);
    }
}
