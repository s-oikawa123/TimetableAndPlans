using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastObserver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsTrigger {  get; private set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsTrigger = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsTrigger = false;
    }
}
