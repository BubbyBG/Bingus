using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System;
using UnityEngine.TestTools;

public class MapTests
{
    [Test]
    public void TestCalculateTimeDifference(){
        /*
            Black Box Unit Test: 
            This test verifies the correctness of the CalculateTimeDifference function in the DayNightCycle class
            Test a positive time range, negative time range, equal times and a time range that crosses midnight
        */

        // toTime > fromTime -> positive
        TimeSpan actual = DayNightCycle.CalculateTimeDifference(TimeSpan.FromHours(10), TimeSpan.FromHours(15));
        TimeSpan expected  = TimeSpan.FromHours(5);

        Assert.AreEqual(actual, expected, "Time difference should be 5 .");
    
        //times are the same 
        actual = DayNightCycle.CalculateTimeDifference(TimeSpan.FromHours(10), TimeSpan.FromHours(10));
        expected = TimeSpan.Zero;
        Assert.AreEqual(expected, actual, "Time difference should be 0.");

        // toTime < fromTime -> negative
        actual = DayNightCycle.CalculateTimeDifference(TimeSpan.FromHours(10), TimeSpan.FromHours(5));
        expected = TimeSpan.FromHours(19);
        Assert.AreEqual(expected, actual, "Time difference should be 19.");

        //midnight change
        actual = DayNightCycle.CalculateTimeDifference(TimeSpan.FromMinutes(1439), TimeSpan.FromMinutes(1)); // 23:59 -> 00:01
        expected = TimeSpan.FromMinutes(2);
        Assert.AreEqual(expected, actual, "Time difference should be 2 minutes.");

    }


    //White-box tests
    [Test]
    public void SunLightRotationTest_isRotating()
    {
        /*
        White-Box Unit Test: This test ensures 100% branch coverage of the RotateSun() function 
        in the DayNightCycle class
        During daytime, the sun rotates from 0° to 180° based on the percentage of time elapsed since sunrise.
        During nighttime, the sun rotates from 180° to 360° based on the percentage of time elapsed since sunset.
        The test manually sets the time, calls RotateSun(), and asserts that the sun’s rotation matches the expected value.


        The Function:
            public void RotateSun(){
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

        */


        //Create test objects
        GameObject sun = new GameObject("Sun");
        Light sunLight = sun.AddComponent<Light>();

        GameObject controller = new GameObject("DayNightCycleController");
        DayNightCycle dayNightCycle = controller.AddComponent<DayNightCycle>();

        //set daynightcycle params
        dayNightCycle.sunLight = sunLight;
        dayNightCycle.sunRiseHour = 6;  
        dayNightCycle.sunSetHour = 18;

        
        //Daytime test
        dayNightCycle.startHour = 12; //set daytime hr
        dayNightCycle.Start(); //6am = 0 degrees 

        dayNightCycle.RotateSun(); //should rotate 90 deg to start time of 12pm 


        float expectedRotation = 90f;
        float actualRotation = sunLight.transform.rotation.eulerAngles.x;

        Assert.AreEqual(expectedRotation, actualRotation, "The sun rotations are not equal during daytime.");



        //Nighttime test
        dayNightCycle.currentTime  =  DateTime.Now.Date + TimeSpan.Zero;
        dayNightCycle.RotateSun(); //should rotate from 12pm to 8pm


        expectedRotation = 270f;

        actualRotation = sunLight.transform.rotation.eulerAngles.x;

        Assert.AreEqual(expectedRotation, actualRotation, "The sun rotations are not equal during the nightime.");


        //Clean up test objects
        GameObject.DestroyImmediate(sun);
        GameObject.DestroyImmediate(controller);
    }


    [Test]
    public void TestUpdateSkyBox(){
        /*
        This tests the functionality of the update skybox function,
        it tests both daytime and night time updates and checks 
        that the materials were uploaded correctly


        FUNCTION WE ARE TESTING
            public void UpdateTimeOfDay()
            {
                currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

                if (timeText != null){
                    timeText.text = currentTime.ToString("HH:mm");
                }
            }
        */
        GameObject controller = new GameObject("DayNightCycleController");
        DayNightCycle dayNightCycle = controller.AddComponent<DayNightCycle>();

        //set daynightcycle params
        dayNightCycle.sunRiseHour = 6;  
        dayNightCycle.sunSetHour = 18;
        dayNightCycle.startHour = 12; //set daytime hr

        //set random materials
        Material daySkybox = new Material(Shader.Find("Standard"));
        Material nightSkybox = new Material(Shader.Find("Standard"));

        // Set the skyboxes on your DayNightCycle.
        dayNightCycle.daySkybox = daySkybox;
        dayNightCycle.nightSkybox = nightSkybox;

        dayNightCycle.Start();

        dayNightCycle.UpdateSkybox();

        Assert.AreEqual(daySkybox, RenderSettings.skybox, "UpdateSkybox did not set the skybox to daySkybox during daytime.");


        //set to nighttime
        dayNightCycle.currentTime  =  DateTime.Now.Date + TimeSpan.Zero;
        dayNightCycle.UpdateSkybox();

        Assert.AreEqual(nightSkybox, RenderSettings.skybox, "UpdateSkybox did not set the skybox to daySkybox during daytime.");

        GameObject.DestroyImmediate(controller);
    }

   
}

