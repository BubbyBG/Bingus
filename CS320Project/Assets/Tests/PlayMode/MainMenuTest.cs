using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;


public class MainMenuTest
{ /*
    
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load the MainMenu scene before each test
        SceneManager.LoadScene("StartMenuScene");
        yield return new WaitForSeconds(1f); // Wait a bit for scene setup
    }


    [UnityTest]
    public IEnumerator TestMainMenuStartGame()
    {
        
        // Integration Test: Ensures that when the scene is loaded from start menu, 
        // the DayNightCycle correctly positions the sun based on time.

        // Units Tested:
        //    - MainMenuManager 
        //    - DayNightCycle 
        

        // Wait for MainMenuManager to appear
        yield return new WaitUntil(() => GameObject.FindObjectOfType<MainMenuManager>() != null);
        MainMenuManager menu = GameObject.FindObjectOfType<MainMenuManager>();

        // Simulate button click
        Button startButton = menu.startButton;
        Assert.IsNotNull(startButton, "Start button not assigned.");
        startButton.onClick.Invoke();

        // Wait for "Main" scene to load
        float timeout = 5f;
        float elapsed = 0f;
        while (SceneManager.GetActiveScene().name != "Main" && elapsed < timeout)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        Assert.AreEqual("Main", SceneManager.GetActiveScene().name, "Scene did not load in time.");

        // Wait for DayNightCycle to appear
        yield return new WaitUntil(() => GameObject.FindObjectOfType<DayNightCycle>() != null);
        DayNightCycle dayNightCycle = GameObject.FindObjectOfType<DayNightCycle>();
        Assert.IsNotNull(dayNightCycle, "DayNightCycle not found.");

        // Find directional light
        Light sunLight = GameObject.FindObjectsOfType<Light>()
            .FirstOrDefault(l => l.type == LightType.Directional);

        Assert.IsNotNull(sunLight, "Directional light not found.");

        // Set time and rotate sun
        dayNightCycle.currentTime = DateTime.Today + TimeSpan.FromHours(12);
        dayNightCycle.RotateSun();

        float expectedRotation = 90f;
        float actualRotation = sunLight.transform.rotation.eulerAngles.x;

        Assert.AreEqual(expectedRotation, actualRotation, 1f, "Sun did not rotate correctly.");
    }*/

}
