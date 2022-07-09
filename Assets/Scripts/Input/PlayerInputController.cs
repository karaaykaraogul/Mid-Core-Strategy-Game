using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using GameEvent.Args;

public class PlayerInputController : MonoBehaviour
{
    public GameObject selectedGameObject;

    private void OnEmptyClick()
    {
        GameEvents.current.OnEmptyClick();
    }

    private void OnBuildingSelected(object sender, OnBuildingSelectedEventArgs e)
    {
        GameEvents.current.OnBuildingSelected(sender, e);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if(hit.collider != null)
            {
                if(hit.collider.GetComponent<IMobile>() != null)
                {
                    if(!hit.collider.GetComponent<IMobile>().GetSelectStatus())
                        hit.collider.GetComponent<IMobile>().Interaction();
                    else
                    {
                        hit.collider.GetComponent<IMobile>().DeselectUnit();
                    }
                }
                else{
                    //to-do implement click on ui and dynamic actions for click types
                    selectedGameObject = hit.collider.gameObject;
                    OnBuildingSelected(this, new OnBuildingSelectedEventArgs{building = selectedGameObject});
                }
            }
            else if(!EventSystem.current.IsPointerOverGameObject())
            {
                GameEvents.current.OnEmptyClick();
            }
        }
    }
}
