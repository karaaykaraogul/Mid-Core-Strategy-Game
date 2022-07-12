using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceManager : Singleton<PlayerResourceManager>
{
    int unitCap = 0;
    int powerCap = 100;
    int currentUnitAmount = 0;
    int currentPowerAmount = 100;

    [SerializeField] Text powerCapText;
    [SerializeField] Text powerCurrentText;
    [SerializeField] Text unitCapText;
    [SerializeField] Text unitCurrentText;

    void OnEnable()
    {
        SetUnitCapText(unitCap.ToString());
        SetPowerCapText(powerCap.ToString());
        SetPowerCurrentText(currentPowerAmount.ToString());
        SetUnitCurrentText(currentUnitAmount.ToString());
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

    public void IncreasePowerCap(int increaseAmount)
    {
        powerCap += increaseAmount;
        SetPowerCapText(powerCap.ToString());
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
}
