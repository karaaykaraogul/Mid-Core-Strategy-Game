using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceManager : Singleton<PlayerResourceManager>
{
    int unitCap = 0;
    int powerCap = 100;
    int buildingCap = 5;
    int currentUnitAmount = 0;
    int currentPowerAmount = 100;
    int currentBuildingAmount = 0;

    [SerializeField] Text powerCapText;
    [SerializeField] Text powerCurrentText;
    [SerializeField] Text unitCapText;
    [SerializeField] Text unitCurrentText;
    [SerializeField] Text buildingCapText;
    [SerializeField] Text buildingCurrentText;
    void Start()
    {
        SetUnitCapText(unitCap.ToString());
        SetPowerCapText(powerCap.ToString());
        SetBuildingCapText(buildingCap.ToString());
        SetPowerCurrentText(currentPowerAmount.ToString());
        SetUnitCurrentText(currentUnitAmount.ToString());
        SetBuildingCurrentText(currentBuildingAmount.ToString());
    }


#region Unit Functions
    public void IncreaseUnitCap(int increaseAmount)
    {
        unitCap += increaseAmount;
        SetUnitCapText(unitCap.ToString());
    }

    public void IncreaseCurrentUnitAmount()
    {
        currentUnitAmount++;
        SetUnitCurrentText(currentUnitAmount.ToString());
    }
    public void DecreaseUnitCap(int increaseAmount)
    {
        unitCap -= increaseAmount;
        SetUnitCapText(unitCap.ToString());
    }
    public void DecreaseCurrentUnitAmount(int amount)
    {
        currentUnitAmount -= amount;
    }
    void SetUnitCapText(string cap)
    {
        unitCapText.text = cap;
    }
    void SetUnitCurrentText(string amount)
    {
        unitCurrentText.text = amount;
    }
    public int GetCurrentUnitAmount()
    {
        return currentUnitAmount;
    }
    public int GetUnitCap()
    {
        return unitCap;
    }
#endregion

#region Power Functions
    public void IncreaseCurrentPowerAmount(int amount)
    {
        currentPowerAmount += amount;
        SetPowerCurrentText(currentPowerAmount.ToString());
    }
    
    public void IncreasePowerCap(int increaseAmount)
    {
        powerCap += increaseAmount;
        SetPowerCapText(powerCap.ToString());
    }
    public void DecreasePowerCap(int increaseAmount)
    {
        powerCap -= increaseAmount;
        SetPowerCapText(powerCap.ToString());
    }
    public void DecreaseCurrentPowerAmount(int amount)
    {
        currentPowerAmount -= amount;
    }
    void SetPowerCapText(string cap)
    {
        powerCapText.text = cap;
    }
    void SetPowerCurrentText(string amount)
    {
        powerCurrentText.text = amount;
    }
    public int GetCurrentPowerAmount()
    {
        return currentPowerAmount;
    }
    public int GetPowerCap()
    {
        return powerCap;
    }
#endregion

#region Unit Functions
    public void IncreaseBuildingCap(int increaseAmount)
    {
        buildingCap += increaseAmount;
        SetBuildingCapText(buildingCap.ToString());
    }

    public void IncreaseCurrentBuildingAmount()
    {
        currentBuildingAmount++;
        SetBuildingCurrentText(currentBuildingAmount.ToString());
    }
    public void DecreaseBuildingCap(int increaseAmount)
    {
        buildingCap -= increaseAmount;
        SetBuildingCapText(buildingCap.ToString());
    }
    public void DecreaseCurrentBuildingAmount()
    {
        currentBuildingAmount--;
        SetBuildingCurrentText(currentBuildingAmount.ToString());
    }
    void SetBuildingCapText(string cap)
    {
        buildingCapText.text = cap;
    }
    void SetBuildingCurrentText(string amount)
    {
        buildingCurrentText.text = amount;
    }
    public int GetCurrentBuildingAmount()
    {
        return currentBuildingAmount;
    }
    public int GetBuildingCap()
    {
        return buildingCap;
    }
#endregion
}
