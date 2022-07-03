using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitProducer
{
    bool doesProduceUnits{get; }
    string producerId{get; }
}
