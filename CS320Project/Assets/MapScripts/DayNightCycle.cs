using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
// using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    //makes the variable appear in the inspector
    [SerializeField] private float timeMultiplier;
    [SerializeField]  private float startHour; 
    private TextMeshProUGUI timeText;


    [SerializeField] private Light sunLight;
    [SerializeField] private float sunRiseHour;
    [SerializeField] private float sunSetHour;

    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;

    private DateTime currentTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;


    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour); //today + offset from startHour

        sunriseTime = TimeSpan.FromHours(sunRiseHour); //TimeSpan converts hours to a time format
        sunsetTime = TimeSpan.FromHours(sunSetHour);

    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateSkybox();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null){
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void RotateSun(){
        float sunLightRotation;

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime){//daytime -> rotates sun from 0 to 180 degrees
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);
            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
            // Debug.Log("rise to set dur" + sunriseToSunsetDuration + " time since " + timeSinceSunrise +"perc "+ percentage);
        }
        else{//Nighttime -> rotates sun from 180 to 360 degrees 
            TimeSpan sunsetToSunriseDur = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);


            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDur.TotalMinutes;
            // Debug.Log("set to rise dur " + sunsetToSunriseDur + " time since " + timeSinceSunset + " perc" + percentage);

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateSkybox(){
        if(currentTime.TimeOfDay >= sunriseTime && currentTime.TimeOfDay < sunsetTime){
            RenderSettings.skybox = daySkybox;
        }
        else{
            RenderSettings.skybox = nightSkybox;
        }
    }

    // private Light UpdateLightSettings(){
        //will add later
    // }
    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan dif = toTime - fromTime;

        if(dif.TotalSeconds < 0)
        {
            dif += TimeSpan.FromHours(24);
        }
        return dif;
    }
}
