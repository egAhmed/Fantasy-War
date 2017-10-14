using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRayCastInterceptor : UnitySingleton<UIRayCastInterceptor> {
    //
    private EventSystem eventSys;
    private GraphicRaycaster graphicRaycaster;
    //
    public bool IsRayCastingGUI(Vector3 mousePosition)
    {
        if (eventSys==null|| graphicRaycaster==null) {
            return false;
        }
        //
        PointerEventData eventData = new PointerEventData(eventSys);
        eventData.pressPosition = mousePosition;
        eventData.position = mousePosition;
        //
        List<RaycastResult> list = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, list);
        //Debug.Log(list.Count);
        return list.Count > 0;
    }
    //
    private void Awake()
    {
        //
        eventSys =GameObject.Find("EventSystem").GetComponent<EventSystem>();
        graphicRaycaster = GameObject.Find("RTSUICanvas").GetComponent<GraphicRaycaster>();
        //
    }
}
