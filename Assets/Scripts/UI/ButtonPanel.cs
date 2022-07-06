using System.Collections;
using System.Collections.Generic;
using BuildingFactoryStatic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] GameObject parentScrollPanel;
    [SerializeField] BuildingButton buttonPrefab;

    private void OnEnable()
    {
        foreach(string name in BuildingFactory.GetBuildingNames())
        {
            var button = Instantiate(buttonPrefab);
            button.gameObject.name = name + "Button";
            button.SetBuildingName(name);
            button.transform.SetParent(parentScrollPanel.transform);
        }
    }

    private void OnDisable()
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
