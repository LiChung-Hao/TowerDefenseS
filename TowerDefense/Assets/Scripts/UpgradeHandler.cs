using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler //includes the mouse hover
{

    //OnMouseEnter works on Colliders and “GUIElement” which is the name for the GUI system that was used before OnGUI.
    //If you use OnMouseEnter on the UI, it won’t work, so you have to use OnPointerEnter instead.
    //And as you may suspect, OnPointerEnter won’t work for colliders, in that case you will need to use OnMouseEnter.
    public bool m_pointerEnter = false;
    public void OnPointerEnter(PointerEventData _pointerEventData)
    {
        m_pointerEnter = true;
        UpgradeManager.s_instance.SetHoveringState(true);

    }

    public void OnPointerExit(PointerEventData _pointerEventData)
    {
        m_pointerEnter = false;
        UpgradeManager.s_instance.SetHoveringState(false);
        gameObject.SetActive(false);
    }
}
