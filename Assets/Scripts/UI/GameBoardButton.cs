using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardButton : MonoBehaviour
{
    private GameObject linkedObject;
    public void SetButtonImage(Sprite sprite)
    {
        gameObject.GetComponent<Image>().sprite = sprite;
    }
    public void SetButtonLink(GameObject linkedGameObject)
    {
        linkedObject = linkedGameObject;
    }
}
