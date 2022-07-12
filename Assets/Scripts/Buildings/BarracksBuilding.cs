using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingFactoryStatic;
public class BarracksBuilding : Building, IBuilding, IUnitProducer
{
    Tilemap.TilemapObject _spawnPoint;
    public Tilemap.TilemapObject spawnPoint {
            get { return _spawnPoint; }
            set { _spawnPoint = value; }
        }
    public override string Name => "Barracks";
    public override string PrefabName => "BarracksPrefab";
    public override int Width => 4;
    public override int Height => 4;
    public string producerId => "barracks";

    public string buildingName 
        { 
            get => this.Name;
        }

    public bool doesProduceUnits => true;

    public int buildingCost => 25;
}
