using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitProducer
{
    Tilemap.TilemapObject spawnPoint{get; set;}
    bool doesProduceUnits{get; }
    string producerId{get; }
}
