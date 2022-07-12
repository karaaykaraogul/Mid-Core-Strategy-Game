using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitFactoryStatic;

public class UnitClient : Singleton<UnitClient>
{
    UnitPathfindingMovementHandler unitPathfindingHandler;

    private void OnEntityCreated()
    {
        GameEvents.current.OnEntityCreated();
    }
    public void InitializeUnit(GameObject unit, Unit unitClass, GameObject initializedFromBuilding)
    {
        Debug.Log("initalizing: " + unit.name);
        Debug.Log("initalizing from: " + initializedFromBuilding.name);
        GameObject newUnit = Instantiate(unit,initializedFromBuilding.transform.position,Quaternion.identity);
        
        if(initializedFromBuilding.GetComponent<IUnitProducer>().spawnPoint != null)
        {
            Vector3 spawnPoint = initializedFromBuilding.GetComponent<IUnitProducer>().spawnPoint.GetTileWorldPositions();
            Debug.Log("spawn point: " + spawnPoint);
            newUnit.GetComponent<IMobile>().SetSpawnPath(spawnPoint);
            Debug.Log("instantiated new unit"); 
        } 
        OnEntityCreated();
        PlayerResourceManager.instance.IncreaseCurrentUnitAmount();
    }
}