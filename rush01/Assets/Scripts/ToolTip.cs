using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string toolTip = "test";

    public void OnPointerEnter(PointerEventData eventData)
    {
        UiManager.ToolTip = toolTip;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UiManager.ToolTip = "";
    }

    private void OnMouseEnter()
    {
        UiManager.ToolTip = toolTip;
    }

    private void OnMouseExit()
    {
        UiManager.ToolTip = "";
    }
}
