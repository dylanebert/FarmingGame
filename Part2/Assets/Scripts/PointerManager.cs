using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PointerManager : MonoBehaviour {
    public static PointerManager instance { get; private set; }
    public static event UnityAction HarvestIconClicked;

    [SerializeField] GameObject blueprint;
    [SerializeField] SpriteRenderer hoverIndicator;
    [SerializeField] GameObject shopButton;
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Vector2 hotSpot;

    PointerMode pointerMode = PointerMode.Default;
    HoverMode hoverMode;
    RaycastHit[] raycastHitResults;
    Vector2 mousePosition;
    Vector3 pointerPosition;
    Plane groundPlane;
    Plot hoverPlot;
    Plot prevHoverPlot;
    LayerMask waterMask;
    LayerMask harvestMask;
    bool pointerOverUI;
    bool hoverWater;
    bool hoverHarvest;

    void Awake() {
        instance = this;
        groundPlane = new Plane(Vector3.up, 0);
        raycastHitResults = new RaycastHit[1];
        waterMask = LayerMask.GetMask("WaterIcon");
        harvestMask = LayerMask.GetMask("HarvestIcon");
        // Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    void OnEnable() {
        UpgradeManager.Changed += HandleUpgradeChanged;
    }

    void OnDisable() {
        UpgradeManager.Changed -= HandleUpgradeChanged;
    }

    void HandleUpgradeChanged() {
        hoverWater = UpgradeManager.GetUnlocked("Watering Can");
        hoverHarvest = UpgradeManager.GetUnlocked("Sickle");
    }

    void Update() {
        pointerOverUI = EventSystem.current.IsPointerOverGameObject();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        hoverMode = HoverMode.Default;
        if(hoverWater && Physics.RaycastNonAlloc(ray, raycastHitResults, float.PositiveInfinity, waterMask) > 0) {
            pointerPosition = raycastHitResults[0].point;
            hoverMode = HoverMode.Water;
        } else if(hoverHarvest && Physics.RaycastNonAlloc(ray, raycastHitResults, float.PositiveInfinity, harvestMask) > 0) {
            pointerPosition = raycastHitResults[0].point;
            hoverMode = HoverMode.Harvest;
        } else if(groundPlane.Raycast(ray, out float enter)) {
            pointerPosition = ray.GetPoint(enter);
        }

        hoverPlot = null;
        if(!pointerOverUI) {
            switch(pointerMode) {
                case PointerMode.Default:
                    if(CropManager.ClampToPlot(pointerPosition, out Plot plot) && plot.currentCrop != null) {
                        hoverPlot = plot;
                        pointerPosition = plot.transform.position;
                        hoverIndicator.transform.position = pointerPosition + Vector3.up * 0.1f;
                        switch(hoverMode) {
                            case HoverMode.Water:
                                hoverIndicator.color = Palette.instance.water;
                                break;
                            case HoverMode.Harvest:
                                if(hoverPlot.harvestCount == 0)
                                    hoverIndicator.color = Palette.instance.seeds;
                                else
                                    hoverIndicator.color = Palette.instance.coins;
                                break;
                            default:
                                hoverIndicator.color = Color.white;
                                break;
                        }
                        hoverIndicator.gameObject.SetActive(true);
                    } else {
                        hoverIndicator.gameObject.SetActive(false);
                    }
                    if(hoverPlot != null) {
                        if(Mouse.current.leftButton.wasPressedThisFrame) {
                            switch(hoverMode) {
                                case HoverMode.Water:
                                    hoverPlot.Water();
                                    break;
                                case HoverMode.Harvest:
                                    hoverPlot.Harvest();
                                    HarvestIconClicked?.Invoke();
                                    break;
                                default:
                                    CropManager.SelectPlot(hoverPlot);
                                    break;
                            }
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
            if(hoverPlot != null && hoverPlot != prevHoverPlot) {
                AudioManager.PlaySound("Tick");
            }
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
                hoverIndicator.gameObject.SetActive(false);
                blueprint.SetActive(false);
                shopButton.SetActive(false);
                break;
            case PointerMode.PlaceCrop:
                hoverIndicator.gameObject.SetActive(false);
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

    public enum HoverMode {
        Default,
        Water,
        Harvest
    }
}
