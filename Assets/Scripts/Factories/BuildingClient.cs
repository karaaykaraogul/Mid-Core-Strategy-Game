using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuildingFactoryStatic;

public class BuildingClient : Singleton<BuildingClient>
{
    public CustomCursor customCursor;
    public bool isInitialized = false;

    private void OnEntityCreated()
    {
        GameEvents.current.OnEntityCreated();
    }

    public void InitializeBuilding(GameObject building, Building buildingClass)
    {
        customCursor.gameObject.SetActive(true);
        customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
        customCursor.GetComponent<SpriteRenderer>().color = Color.green;
        Cursor.visible = false;
        isInitialized = true;
        StartCoroutine(PlacingBuilding(building, buildingClass));
    }

    IEnumerator PlacingBuilding(GameObject building, Building buildingClass)
    {
        while(isInitialized)
        {
            customCursor.gameObject.SetActive(true);
            if(!GridManager.instance.GetGridAvailability(buildingClass.Width,buildingClass.Height))
            {
                customCursor.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                customCursor.GetComponent<SpriteRenderer>().color = Color.green;
            }
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 buildingPos = GridManager.instance.GetClickedGridPositions();
                bool isGridAvailable = GridManager.instance.GetGridAvailability(buildingClass.Width,buildingClass.Height);
                if(isGridAvailable)
                {
                    GridManager.instance.SetGridBuilt(buildingClass.Width, buildingClass.Height);
                    GameObject newBuilding = Instantiate(building,buildingPos,Quaternion.identity);
                    if(newBuilding.GetComponent<IUnitProducer>() != null)
                    {
                        newBuilding.GetComponent<IUnitProducer>().spawnPoint = GridManager.instance.GetBuildingSpawnPoint(buildingPos);
                        Debug.Log("spawn point x and y: " + newBuilding.GetComponent<IUnitProducer>().spawnPoint.GetX() + " , " + newBuilding.GetComponent<IUnitProducer>().spawnPoint.GetY()); 
                    }
                    
                    PlayerResourceManager.instance.IncreaseCurrentBuildingAmount(); 
                }
                isInitialized = false;
            }
            else if(Input.GetMouseButtonDown(1))
            {
                Debug.Log("iptal");
                isInitialized = false;
            }
            yield return null;
        }
        OnEntityCreated();
        customCursor.gameObject.SetActive(false);
        Cursor.visible = true;
        yield return null;
    }
}
