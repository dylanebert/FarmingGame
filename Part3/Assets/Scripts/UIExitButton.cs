using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIExitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
    public UnityEvent onClick;

    [SerializeField] Image icon;

    Sequence sequence;

    void OnDisable() {
        sequence?.Kill();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(icon.rectTransform.DOScale(1.1f, 0.1f));
        AudioManager.PlaySound("Tick");
    }

    public void OnPointerExit(PointerEventData eventData) {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(icon.rectTransform.DOScale(1, 0.1f));
    }

    public void OnPointerDown(PointerEventData eventData) {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(icon.rectTransform.DOScale(0.9f, 0.1f));
    }

    public void OnPointerUp(PointerEventData eventData) {
        sequence?.Kill();
        sequence = DOTween.Sequence();
        sequence.Append(icon.rectTransform.DOScale(1, 0.1f));
    }

    public void OnPointerClick(PointerEventData eventData) {
        AudioManager.PlaySound("Click");
        onClick?.Invoke();
    }
}
