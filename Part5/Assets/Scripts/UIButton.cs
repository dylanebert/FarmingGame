using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
    public event UnityAction<UIButton> Clicked;
    public event UnityAction<UIButton> Hovered;
    public event UnityAction<UIButton> Unhovered;

    public UnityEvent onClick;

    [SerializeField] protected RectTransform contents;
    [SerializeField] protected Image border;

    protected Sequence hoverSequence;
    protected Sequence pressSequence;

    protected virtual void OnDisable() {
        hoverSequence?.Kill();
        pressSequence?.Kill();
        if(border != null)
            border.color = new Color(border.color.r, border.color.g, border.color.b, 0);
        contents.localScale = Vector3.one;
    }

    public virtual void OnPointerEnter(PointerEventData eventData) {
        AudioManager.PlaySound("Tick");
        Hovered?.Invoke(this);
        if(border == null) return;
        hoverSequence?.Kill();
        hoverSequence = DOTween.Sequence();
        hoverSequence.Append(border.DOFade(1, 0.1f));
    }

    public virtual void OnPointerExit(PointerEventData eventData) {
        Unhovered?.Invoke(this);
        if(border == null) return;
        hoverSequence?.Kill();
        hoverSequence = DOTween.Sequence();
        hoverSequence.Append(border.DOFade(0, 0.1f));
    }

    public virtual void OnPointerDown(PointerEventData eventData) {
        AudioManager.PlaySound("Click");
        pressSequence?.Kill();
        pressSequence = DOTween.Sequence();
        pressSequence.Append(contents.DOScale(0.9f, 0.1f));
    }

    public virtual void OnPointerUp(PointerEventData eventData) {
        pressSequence?.Kill();
        pressSequence = DOTween.Sequence();
        pressSequence.Append(contents.DOScale(1, 0.1f));
    }

    public virtual void OnPointerClick(PointerEventData eventData) {
        onClick?.Invoke();
        Clicked?.Invoke(this);
    }

    public virtual void SetInteractive(bool interactive) {
        
    }
}
