using System;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class CropManager : MonoBehaviour {
    public static CropManager instance { get; private set; }

    [SerializeField] UIButton cancelPlaceCropsButton;
    [SerializeField] UIButton cancelFocusButton;
    [SerializeField] CropInfoDisplay cropInfoDisplay;

    public Plot[] plots;

    public static OrderedDictionary crops { get; private set; }

    ICrop placingCrop;
    Plot focusedPlot;
    int expansion;
    bool placed;

    void Awake() {
        instance = this;
        crops = new OrderedDictionary();
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(ICrop).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(x => (ICrop)Activator.CreateInstance(x))
            .OrderBy(x => x.menuIndex).ToList().ForEach(x => crops.Add(x.name, x));
        ExpandInternal();
    }

    public static void Expand() {
        instance.ExpandInternal();
    }

    void ExpandInternal() {
        expansion++;
        int crops = Mathf.Clamp(expansion* 3, 0, plots.Length);
        for(int i = 0; i < crops; i++)
            plots[i].SetActive(true);
    }

    public static void SelectPlot(Plot plot) {
        if(instance.focusedPlot == plot) return;
        CameraController.SetFocus(plot);
        AudioManager.PlaySound("Click");
        instance.cropInfoDisplay.Show(plot);
        PointerManager.SetPointerMode(PointerManager.PointerMode.FocusCrop);
        instance.cancelFocusButton.gameObject.SetActive(true);
        instance.focusedPlot = plot;
    }

    public static void DeselectPlot() {
        if(instance.focusedPlot == null) return;
        CameraController.SetFocus(null);
        instance.cropInfoDisplay.Hide();
        PointerManager.SetPointerMode(PointerManager.PointerMode.Default);
        instance.cancelFocusButton.gameObject.SetActive(false);
        instance.focusedPlot = null;
    }

    public static void PlaceCrop(Plot plot) {
        instance.PlaceCropInternal(plot);
    }

    public static void RemoveCrop(Plot plot) {
        plot.Remove();
    }

    void PlaceCropInternal(Plot plot) {
        placed = true;
        plot.Place(placingCrop);
        CameraController.Shake();
        AudioManager.PlaySound("Place");
        Clear();
    }

    public static void BeginPlacingCrop(string cropName) {
        instance.BeginPlacingCropInternal(cropName);
    }

    public static void BeginPlacingCrop(ICrop crop) {
        instance.BeginPlacingCropInternal(crop);
    }

    void BeginPlacingCropInternal(string cropName) {
        if(!crops.Contains(cropName)) {
            Debug.LogError("CropManager: No crop with name " + cropName);
            return;
        }
        ICrop crop = (ICrop)crops[cropName];
        BeginPlacingCropInternal(crop);
    }

    void BeginPlacingCropInternal(ICrop crop) {
        DeselectPlot();
        placingCrop = crop;
        PointerManager.SetPointerMode(PointerManager.PointerMode.PlaceCrop);
        cancelPlaceCropsButton.gameObject.SetActive(true);
        HighlightCrops();
    }

    public static void Clear() {
        instance.ClearInternal();
    }

    void ClearInternal() {
        if(!placed && placingCrop != null) {
            ShopManager.Refund(placingCrop);
        }
        placed = false;
        placingCrop = null;
        PointerManager.SetPointerMode(PointerManager.PointerMode.Default);
        cancelPlaceCropsButton.gameObject.SetActive(false);
        UnhighlightCrops();
    }

    void HighlightCrops() {
        for(int i = 0; i < plots.Length; i++) {
            if(plots[i].active && plots[i].currentCrop == null)
                plots[i].highlight.SetActive(true);
        }
    }

    void UnhighlightCrops() {
        for(int i = 0; i < plots.Length; i++) {
            plots[i].highlight.SetActive(false);
        }
    }

    public static bool ClampToPlot(Vector3 position, out Plot plot) {
        return instance.ClampToPlotInternal(position, out plot);
    }

    bool ClampToPlotInternal(Vector3 position, out Plot plot) {
        for(int i = 0; i < plots.Length; i++) {
            if(plots[i].active && plots[i].ClampToPlot(position)) {
                plot = plots[i];
                return true;
            }
        }
        plot = null;
        return false;
    }
}
