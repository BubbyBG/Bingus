using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;
    public Slider thresholdSlider;
    public Image sliderFill;
    public Color fillColor;


    public void SetValueRange(int max)
    {
        // slider = new GameObject("Slider").AddComponent<Slider>(); // for testing
        // thresholdSlider = new GameObject("ThresholdSlider").AddComponent<Slider>(); // for tetsing

        slider.maxValue = max; // overall range represented in the status HUD (black bar)
        slider.minValue = 0;
        thresholdSlider.maxValue = max;
        thresholdSlider.minValue = 0;
    }

    public void SetStatus(int status, int thresholdStatus, string htmlColor)
    {
        // slider = new GameObject("Slider").AddComponent<Slider>(); // for testing
        // thresholdSlider = new GameObject("ThresholdSlider").AddComponent<Slider>(); // for tetsing
        ColorUtility.TryParseHtmlString(htmlColor, out fillColor);
        sliderFill.color = fillColor;
        slider.value = status;
        thresholdSlider.value = thresholdStatus;
    }
}