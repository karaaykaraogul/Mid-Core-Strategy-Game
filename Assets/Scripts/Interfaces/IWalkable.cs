using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWalkable
{
    bool isWalkable{get; set;}
    bool isOccupiedByUnit{get; set;}
}
