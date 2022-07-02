using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitFactoryStatic;

public class UnitClient : Singleton<UnitClient>
{
    
    void OnEnable()
    {
        UnitFactory.GetUnitsByProducer();
    }
}