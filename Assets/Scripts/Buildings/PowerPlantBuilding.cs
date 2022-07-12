using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingFactoryStatic;
public class PowerPlantBuilding : Building, IBuilding, IProducerBuilding
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

    public int produceAmount => 5;
    private float nextIncreaseTime;
    public float timeBtwIncreases = 5f;

    void FixedUpdate()
    {
        if(Time.time > nextIncreaseTime && (PlayerResourceManager.instance.GetCurrentPowerAmount() < PlayerResourceManager.instance.GetPowerCap()))
        {
            nextIncreaseTime = Time.time + timeBtwIncreases;
            PlayerResourceManager.instance.IncreaseCurrentPowerAmount(produceAmount);
        }
    }
}
