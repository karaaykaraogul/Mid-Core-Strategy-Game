using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvent.Args;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }
    public event EventHandler<OnBuildingSelectedEventArgs> onBuildingSelected;
    public event Action onEmptyClick;
    public event Action onUnitClick;
    public event Action onEntityCreated;

    public void OnEntityCreated()
    {
        if(onEntityCreated != null)
        {
            onEntityCreated();
        }
    }
    public void OnEmptyClick()
    {
        if(onEmptyClick != null)
        {
            onEmptyClick();
        }
    }
    public void OnUnitClick()
    {
        if(onUnitClick != null)
        {
            onUnitClick();
        }
    }
    public void OnBuildingSelected(object sender, OnBuildingSelectedEventArgs e)
    {
        if(onBuildingSelected != null)
        {
            onBuildingSelected(sender, e);
        }
    }
}
