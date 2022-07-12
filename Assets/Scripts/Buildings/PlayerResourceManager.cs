using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceManager : Singleton<PlayerResourceManager>
{
    int soldierCap = 0;
    int powerCap = 100;
    int currentSoldierAmount = 0;
    int currentPowerAmount = 100;

    public void IncreaseSoldierCap(int increaseAmount)
    {
        soldierCap += increaseAmount;
        Debug.Log(soldierCap);
    }

    public void IncreasePowerCap(int increaseAmount)
    {
        powerCap += increaseAmount;
        Debug.Log(powerCap);
    }

    public void DecreaseSoldierCap(int increaseAmount)
    {
        soldierCap -= increaseAmount;
        Debug.Log(soldierCap);
    }

    public void DecreasePowerCap(int increaseAmount)
    {
        powerCap -= increaseAmount;
        Debug.Log(powerCap);
    }
}
