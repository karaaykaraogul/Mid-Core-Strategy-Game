using System;
using System.Collections;
using System.Collections.Generic;
using UnitFactoryStatic;
using UnityEngine;
using Utils;

public class SoldierUnit : Unit, IBarrackUnit, IMobile
{
    [SerializeField]UnitPathfindingMovementHandler unitPathfindingHandler;
    public override string Name => "Soldier Unit";
    
    public override string ProducerId => "barracks";

    public override string PrefabName => "SoldierPrefab";

    public override int Health => throw new System.NotImplementedException();

    public override float Speed => 3f;

    public bool isSelected = false;
    
    Color baseColor;
    Color selectedColor;
    
    void Awake()
    {
        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        selectedColor = Color.green;
    }

    void OnEnable()
    {
        PlayerInputController inputController = FindObjectOfType<PlayerInputController>();
        inputController.OnEmptyClick += CancelInteraction;
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && isSelected)
        {
            unitPathfindingHandler.SetTargetPosition(UtilsClass.GetMouseWorldPosition(), GetSpeed());
        }
    }

    public void Interaction()
    {
        gameObject.GetComponent<SpriteRenderer>().color = selectedColor;
        this.isSelected = true;
    }
    
    public void DeselectUnit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = baseColor;
        this.isSelected = false;
    }

    public void CancelInteraction(object sender, EventArgs e)
    {
        gameObject.GetComponent<SpriteRenderer>().color = baseColor;
        this.isSelected = false;
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public void SetPath()
    {
        throw new System.NotImplementedException();
    }

    public bool GetSelectStatus()
    {
        return isSelected;
    }
}
