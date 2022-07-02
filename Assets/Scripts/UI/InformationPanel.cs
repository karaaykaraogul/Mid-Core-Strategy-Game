using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] GameObject selectedObjectInfo;
    [SerializeField] Text selectedObjectText;
    [SerializeField] Image selectedObjectImage;
    private void OnEnable()
    {
        PlayerInputController inputController = FindObjectOfType<PlayerInputController>();
        inputController.OnBuildingSelected += BuildingSelectedInfo;
        inputController.OnEmptyClick += EmptyClickInfo;
    }

    private void BuildingSelectedInfo(object sender, PlayerInputController.OnBuildingSelectedEventArgs e)
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
            Debug.Log("I produce units! ");
        }
    }

    private void EmptyClickInfo(object sender, EventArgs e)
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
    
}
