using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    private float pressScale = 0.97f;
    private float duration = 0.08f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, originalScale * pressScale, duration).setEaseOutSine();

        // 🎵 تشغيل الصوت هنا
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayClickSound();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, originalScale, duration).setEaseOutSine();
    }
}
