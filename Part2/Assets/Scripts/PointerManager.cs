using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PointerManager : MonoBehaviour {
    public static PointerManager instance { get; private set; }

    [SerializeField] GameObject blueprint;
    [SerializeField] GameObject hoverIndicator;
    [SerializeField] GameObject shopButton;

    PointerMode pointerMode = PointerMode.Default;
    Vector2 mousePosition;
    Vector3 pointerPosition;
    Plane groundPlane;
    Plot hoverPlot;
    Plot prevHoverPlot;
    bool pointerOverUI;

    void Awake() {
        instance = this;
        groundPlane = new Plane(Vector3.up, 0);
    }

    void Update() {
        pointerOverUI = EventSystem.current.IsPointerOverGameObject();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if(groundPlane.Raycast(ray, out float enter)) {
            pointerPosition = ray.GetPoint(enter);
        }

        hoverPlot = null;
        if(!pointerOverUI) {
            switch(pointerMode) {
                case PointerMode.Default:
                    if(CropManager.ClampToPlot(pointerPosition, out Plot plot) && plot.currentCrop != null) {
                        hoverPlot = plot;
                        pointerPosition = plot.transform.position;
                        hoverIndicator.transform.position = pointerPosition;
                        hoverIndicator.SetActive(true);
                    } else {
                        hoverIndicator.SetActive(false);
                    }
                    if(hoverPlot != null) {
                        if(Mouse.current.leftButton.wasPressedThisFrame) {
                            AudioManager.PlaySound("Click");
                            CropManager.SelectPlot(hoverPlot);
                        }
                    }
                    break;
                case PointerMode.PlaceCrop:
                    if(CropManager.ClampToPlot(pointerPosition, out plot) && plot.currentCrop == null) {
                        hoverPlot = plot;
                        pointerPosition = plot.transform.position;
                    }
                    blueprint.transform.position = pointerPosition;
                    if(hoverPlot != null && Mouse.current.leftButton.wasPressedThisFrame) {
                        CropManager.PlaceCrop(hoverPlot);
                    }
                    break;
                default:
                    break;
            }
            if(hoverPlot != null && hoverPlot != prevHoverPlot)
                AudioManager.PlaySound("Tick");
        }
        prevHoverPlot = hoverPlot;
    }

    public static void SetPointerMode(PointerMode pointerMode) {
        instance.SetPointerModeInternal(pointerMode);
    }

    void SetPointerModeInternal(PointerMode pointerMode) {
        this.pointerMode = pointerMode;
        UIShop.instance.Hide();
        switch(pointerMode) {
            case PointerMode.Default:
                blueprint.SetActive(false);
                shopButton.SetActive(true);
                break;
            case PointerMode.FocusCrop:
                hoverIndicator.SetActive(false);
                blueprint.SetActive(false);
                shopButton.SetActive(false);
                break;
            case PointerMode.PlaceCrop:
                hoverIndicator.SetActive(false);
                blueprint.SetActive(true);
                shopButton.SetActive(false);
                break;
        }
    }

    public void Aim(InputAction.CallbackContext context) {
        mousePosition = context.ReadValue<Vector2>();
    }

    public enum PointerMode {
        Default,
        PlaceCrop,
        FocusCrop,
    }
}
