using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEventsHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public UnityEvent OnTouchStart;
    public UnityEvent OnTouchEnd;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouchStart.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnTouchEnd.Invoke();
    }
}