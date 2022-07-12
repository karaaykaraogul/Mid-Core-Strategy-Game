using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingFactoryStatic;
public class PowerPlantBuilding : Building, IBuilding
{
    public override string Name => "Power Plant";
    public override string PrefabName => "PowerPlantPrefab";
    public override int Width => 2;
    public override int Height => 3;
    
    public string buildingName 
        { 
            get => this.Name;
        }

    public int buildingCost => 50;
}
