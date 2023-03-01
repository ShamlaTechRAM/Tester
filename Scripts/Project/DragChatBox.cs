using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragChatBox : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private RectTransform chatboxRectTransform;
    [SerializeField]
    private Canvas canvas;
    public void OnDrag(PointerEventData eventData) {
        chatboxRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
