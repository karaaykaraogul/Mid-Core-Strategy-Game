using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    public GameObject selectedGameObject;

    public event EventHandler<OnBuildingSelectedEventArgs> OnBuildingSelected;
    public event EventHandler OnEmptyClick;
    public class OnBuildingSelectedEventArgs : EventArgs
    {
        public GameObject building;
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if(hit.collider != null)
            {
                //to-do implement click on ui and dynamic actions for click types
                selectedGameObject = hit.collider.gameObject;
                OnBuildingSelected?.Invoke(this, new OnBuildingSelectedEventArgs{building = selectedGameObject});
            }
            else if(!EventSystem.current.IsPointerOverGameObject())
            {
                OnEmptyClick?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
