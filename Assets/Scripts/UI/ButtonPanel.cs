using System.Collections;
using System.Collections.Generic;
using BuildingFactoryStatic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] BuildingButton buttonPrefab;

    private void OnEnable()
    {
        foreach(string name in BuildingFactory.GetBuildingNames())
        {
            var button = Instantiate(buttonPrefab);
            button.gameObject.name = name + "Button";
            button.SetBuildingName(name);
            button.transform.SetParent(transform);
        }
    }
}
