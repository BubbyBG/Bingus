using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class Test1
{
    [UnityTest]
    public IEnumerator IncreaseSkillPointsTest() //black box unit test of IncreaseSkillPoints function from PlayerSkillSystem.cs
    {
        var gameOjbect = new GameObject();
        // var skillSystem = gameOjbect.AddComponent<PlayerSkillSystem>();

        var pointText = new GameObject();
        var textComponent = pointText.AddComponent<Text>();
        // skillSystem.pointSystem = textComponent;
        yield return null;

        // Assert.AreEqual(5, skillSystem.SkillPoints);
        // Assert.AreEqual("POINTS: 5", skillSystem.pointSystem.text);
        // skillSystem.EnemyDeaths = 4;
        // yield return null;

        // Assert.AreEqual(6, skillSystem.SkillPoints);
        // Assert.AreEqual("POINTS: 6", skillSystem.pointSystem.text);
    }


    [UnityTest]
    public IEnumerator HealthTest() //white box unit test //100% branch coverage
    {
        var gameObject = new GameObject();
        var playerInfo = gameObject.AddComponent<PlayerInfo>();
        yield return null;

        playerInfo.ChangeHealth(10); // current health goes above threshold branch
        Assert.AreEqual(playerInfo.healthThreshold, playerInfo.currentHealth);
        yield return null;

        playerInfo.ChangeHealth(-100); // current health goes below threshold branch
        Assert.AreEqual(0, playerInfo.currentHealth);
        yield return null;

        playerInfo.ChangeHealth(10); // current helath is within bounds branch
        Assert.AreEqual(10, playerInfo.currentHealth);
    }

    [UnityTest]
    public IEnumerator staminaTest() //white box unit test // 100% branch coverage
    {
        var gameObject = new GameObject();
        var playerInfo = gameObject.AddComponent<PlayerInfo>();
        yield return null;

        playerInfo.currentStamina = 30;
        playerInfo.ChangeStamina(-40); // current stamina goes below zero branch
        Assert.AreEqual(0, playerInfo.currentStamina);
        yield return null;

        playerInfo.currentStamina = 30;
        playerInfo.ChangeStamina(30); // current stamina goes above threshold branch
        Assert.AreEqual(playerInfo.staminaThreshold, playerInfo.currentStamina);
        yield return null;

        playerInfo.currentStamina = 30;
        playerInfo.ChangeStamina(15); // current stamina is within bounds branch
        Assert.AreEqual(45, playerInfo.currentStamina);
    }

    [UnityTest]
    public IEnumerator PlayerInfoSkillSystemTest() // Integration test // testing PlayerInfo class and PlayerSkillSystem class.
    {
        var gameOjbect = new GameObject();
        var playerInfo = gameOjbect.AddComponent<PlayerInfo>();
        var skillSystem = gameOjbect.AddComponent<PlayerSkillSystem>();
        var pointText = new GameObject();
        var textComponent = pointText.AddComponent<Text>();
        // skillSystem.pointSystem = textComponent;
        yield return null;

        // skillSystem.skillToHealth();
        // skillSystem.skillToStamina();
        // skillSystem.skillToDamage();
        // yield return null;

        Assert.AreEqual(60, playerInfo.healthThreshold);
        Assert.AreEqual(60, playerInfo.staminaThreshold);
        Assert.AreEqual(6, playerInfo.damageValue);
        // Assert.AreEqual(2, skillSystem.SkillPoints);
        // Assert.AreEqual("POINTS: 2", skillSystem.pointSystem.text);
    }
}
