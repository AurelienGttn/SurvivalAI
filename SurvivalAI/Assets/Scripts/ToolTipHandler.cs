using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    public GameObject toolTipUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toolTipUI != null)
        {
            toolTipUI.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (toolTipUI != null)
        {
            toolTipUI.SetActive(false);
        }
    }
}
