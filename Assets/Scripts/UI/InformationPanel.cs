using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitFactoryStatic;
using GameEvent.Args;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] UnitButton unitButtonPrefab;
    [SerializeField] GameObject selectedObjectInfo;
    [SerializeField] Text selectedObjectText;
    [SerializeField] Image selectedObjectImage;
    private void OnEnable()
    {
        PlayerInputController inputController = FindObjectOfType<PlayerInputController>();
        GameEvents.current.onBuildingSelected += BuildingSelectedInfo;
        GameEvents.current.onEmptyClick += EmptyClickInfo;
    }

    private void BuildingSelectedInfo(object sender, OnBuildingSelectedEventArgs e)
    {
        RemoveChildObjects();
        var parentInfoObject = Instantiate(selectedObjectInfo);
        var infoText = Instantiate(selectedObjectText);
        var infoImage = Instantiate(selectedObjectImage);
        infoText.text = e.building.GetComponent<IBuilding>().buildingName;
        infoImage.sprite = e.building.GetComponent<SpriteRenderer>().sprite;
        infoText.transform.SetParent(parentInfoObject.transform);
        infoImage.transform.SetParent(parentInfoObject.transform);
        parentInfoObject.transform.SetParent(transform);
        if(e.building.GetComponent<IUnitProducer>() != null)
        {
            Dictionary<string, Unit> units = UnitFactory.GetUnitsByProducer();
            foreach(var unit in units)
            {
                if(unit.Key == e.building.GetComponent<IUnitProducer>().producerId)
                {
                    //unitButtonPrefab = new UnitButton(e.building);
                    UnitButton button = Instantiate(unitButtonPrefab);
                    button.Init(e.building);
                    unitButtonPrefab.gameObject.name = name + "Button";
                    button.GetComponent<Image>().sprite = Resources.Load<SpriteRenderer>(unit.Value.PrefabName).sprite;
                    button.SetUnitName(unit.Value.Name);
                    button.transform.SetParent(transform);
                }
            }
        }

    }

    private void EmptyClickInfo()
    {
        RemoveChildObjects();
    }

    private void RemoveChildObjects()
    {
        List<GameObject> childObjects = new List<GameObject>();
        int childCount = gameObject.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            childObjects.Add(gameObject.transform.GetChild(i).gameObject);
        }
        foreach(var cObject in childObjects)
        {
            Destroy(cObject);
        }
    }
    
    private void OnDisable()
    {
        GameEvents.current.onBuildingSelected -= BuildingSelectedInfo;
        GameEvents.current.onEmptyClick -= EmptyClickInfo;
    }
}
