using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingFactoryStatic;

public class BulldozerButton : MonoBehaviour
{
    bool isDeleting = false;
    GameObject hoverOverObject;
    public void DeleteBuilding()
    {
        isDeleting = true;
        StartCoroutine(DeletingBuilding());
    }

    IEnumerator DeletingBuilding()
    {
        while(isDeleting)
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);
            if(hit.collider != null)
            {
                if(hit.collider.GetComponent<Building>() != null)
                {
                    if(hoverOverObject != null)
                    {
                        hoverOverObject.GetComponent<SpriteRenderer>().color = hit.collider.gameObject != hoverOverObject ? Color.white : Color.red;
                    }
                    hoverOverObject = hit.collider.gameObject;
                    hoverOverObject.GetComponent<SpriteRenderer>().color = Color.red;
                    
                    if(Input.GetMouseButtonDown(0))
                    {
                        isDeleting = false;
                        if(hoverOverObject.GetComponent<ICapBuilding>() != null)
                        {
                            hoverOverObject.GetComponent<ICapBuilding>().DecreaseCap();
                        }
                        GridManager.instance.SetGridBuildable(hoverOverObject.GetComponent<Building>().Width,hoverOverObject.GetComponent<Building>().Height, hoverOverObject.transform.position);
                        Destroy(hoverOverObject);
                        PlayerResourceManager.instance.DecreaseCurrentBuildingAmount();
                    }
                }
            }
            else if(hoverOverObject != null)
            {
                hoverOverObject.GetComponent<SpriteRenderer>().color = Color.white;
            }

            if(Input.GetMouseButtonDown(1))
            {
                isDeleting = false;
                if(hoverOverObject != null)
                {
                    hoverOverObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            yield return null;
        }
        yield return null;
    }
}
