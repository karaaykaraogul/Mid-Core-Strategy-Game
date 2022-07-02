using System.Collections;
using System.Collections.Generic;
using UnitFactoryStatic;
using UnityEngine;

public class SoldierUnit : Unit, IBarrackUnit
{
    public override string Name => "Soldier Unit";

    public override string PrefabName => throw new System.NotImplementedException();

    public override int Health => throw new System.NotImplementedException();

    public override int Speed => throw new System.NotImplementedException();
}
