using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingFactoryStatic;
using UnitFactoryStatic;

public class GameBoardPanel : MonoBehaviour
{
    [SerializeField] GameObject parentBuildingPanel;
    [SerializeField] GameObject parentUnitPanel;
    [SerializeField] GameBoardButton buttonPrefab;
    void Start()
    {
        GameEvents.current.onEntityCreated += OnEntityCreated;
    }

    private void OnEntityCreated()
    {
        RemoveChildObjects(parentBuildingPanel);
        RemoveChildObjects(parentUnitPanel);
        var buildings = FindObjectsOfType<Building>();
        foreach(var building in buildings)
        {
            var button = Instantiate(buttonPrefab);
            button.gameObject.name = building.name + "Button";
            button.SetButtonImage(building.GetComponent<SpriteRenderer>().sprite);
            button.SetButtonLink(building.gameObject);
            button.transform.SetParent(parentBuildingPanel.transform);
        }

        var units = FindObjectsOfType<Unit>();
        foreach(var unit in units)
        {
            var button = Instantiate(buttonPrefab);
            button.gameObject.name = name + "Button";
            button.SetButtonImage(unit.GetComponent<SpriteRenderer>().sprite);
            button.SetButtonLink(unit.gameObject);
            button.transform.SetParent(parentUnitPanel.transform);
        }
    }

    private void RemoveChildObjects(GameObject deletedFromGameObject)
    {
        List<GameObject> childObjects = new List<GameObject>();
        int childCount = deletedFromGameObject.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            childObjects.Add(deletedFromGameObject.transform.GetChild(i).gameObject);
        }
        foreach(var cObject in childObjects)
        {
            Destroy(cObject);
        }
    }

    void OnDisable()
    {
        GameEvents.current.onEntityCreated -= OnEntityCreated;
    }
}
