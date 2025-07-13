using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFeedback : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 0.94f;
        //LeanTween.scale(gameObject, Vector3.one, 0.15f).setEaseOutBack();
    }
}
