using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;

    public void SetValueRange(int max)
    {
        slider.maxValue = max; // overall range represented in the status HUD (black bar)
        slider.minValue = 0;
    }

    public void SetStatus(int status)
    {
        slider.value = status;
    }
}