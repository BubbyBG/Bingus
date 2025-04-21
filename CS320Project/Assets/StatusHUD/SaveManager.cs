using UnityEngine;
using System.IO;


public static class SaveManager
{
    public static bool isLoadGame = false; // specified by deathscreen or mainmenu manager before starting scene
    private static string playerSavePath = Application.persistentDataPath + "/player.json";
    private static string skillSavePath = Application.persistentDataPath + "/skill.json";


    public static void SaveGame(PlayerInfo player, PlayerSkillSystem skillSystem) //saves all game files
    {
        PlayerSave(player);
        SkillSave(skillSystem);
    }
    public static void LoadGameSave(PlayerInfo player, PlayerSkillSystem skillSystem) //loads all game files
    {
        PlayerLoad(player);
        SkillLoad(skillSystem);
    }
    public static void DeleteSaveData() //deletes all game files
    {
        if (File.Exists(playerSavePath))
        {
            File.Delete(playerSavePath);
            Debug.Log($"Deleted player save at: {playerSavePath}");
        }
        if (File.Exists(skillSavePath))
        {
            File.Delete(skillSavePath);
            Debug.Log($"Deleted skill save at: {skillSavePath}");
        }
    }
    public static bool IsLoadAvailable()
    {
        return ((File.Exists(playerSavePath)) || (File.Exists(skillSavePath)));
    }


    public static void PlayerSave(PlayerInfo player) // move playerinfo data to json file
    {
        var playerData = new PlayerData(player);
        string jsonData = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(playerSavePath, jsonData);
        Debug.Log($"Player data saved at: {playerSavePath}");
    }

    public static bool PlayerLoad(PlayerInfo player) // get json file data into playerload
    {
        if (!File.Exists(playerSavePath))
        {
            Debug.Log($"Can't load file at: {playerSavePath}");
            return false;
        }
        string jsonData = File.ReadAllText(playerSavePath);
        var playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        playerData.ApplyPlayerData(player);
        Debug.Log($"Player data loaded from: {playerSavePath}");
        return true;
    }


    public static void SkillSave(PlayerSkillSystem skillSystem)
    {
        var skillData = new SkillData(skillSystem);
        string jsonData = JsonUtility.ToJson(skillData, true);
        File.WriteAllText(skillSavePath, jsonData);
        Debug.Log($"Skill data saved at: {skillSavePath}");
    }

    public static bool SkillLoad(PlayerSkillSystem skillSystem)
    {
        if (!File.Exists(skillSavePath))
        {
            Debug.Log($"Can't load file at: {skillSavePath}");
            return false;
        }
        string jsonData = File.ReadAllText(skillSavePath);
        var skillData = JsonUtility.FromJson<SkillData>(jsonData);
        skillData.ApplySkillData(skillSystem);
        Debug.Log($"Player data loaded from: {skillSavePath}");
        return true;
    }
}


[System.Serializable]
public class PlayerData
{
    public int healthThreshold;
    public int currentHealth;
    public int staminaThreshold;
    public int currentStamina;
    public int damageValue;
    public int damageBonus;
    public string currentHealthColor;
    public Vector3 playerPosition;
    public Vector3 playerDirection;
    public Vector3 playerRotation;

    public PlayerData(PlayerInfo playerInfo) // construct
    {
        healthThreshold = playerInfo.healthThreshold;
        currentHealth = playerInfo.currentHealth;
        staminaThreshold = playerInfo.staminaThreshold;
        currentStamina = playerInfo.currentStamina;
        damageValue = playerInfo.damageValue;
        damageBonus = playerInfo.damageBonus;
        currentHealthColor = playerInfo.currentHealthColor;
        playerPosition = playerInfo.transform.position;
        playerDirection = playerInfo.transform.forward;
        playerRotation = playerInfo.transform.eulerAngles;
    }
    public void ApplyPlayerData(PlayerInfo playerInfo)
    {
        playerInfo.healthThreshold = healthThreshold;
        playerInfo.currentHealth = currentHealth;
        playerInfo.staminaThreshold = staminaThreshold;
        playerInfo.currentStamina = currentStamina;
        playerInfo.damageValue = damageValue;
        playerInfo.damageBonus = damageBonus;
        playerInfo.currentHealthColor = currentHealthColor;
        playerInfo.transform.position = playerPosition;
        playerInfo.transform.forward = playerDirection;
        playerInfo.transform.eulerAngles = playerRotation;

        playerInfo.healthBar.SetStatus(currentHealth, healthThreshold, currentHealthColor);
        playerInfo.staminaBar.SetStatus(currentStamina, staminaThreshold, "#CEC400");
        playerInfo.damageBar.SetStatus(damageValue, damageBonus, "#C231CD");
    }
}


[System.Serializable]
public class SkillData
{
    public int skillPoints;
    public SkillData(PlayerSkillSystem skillSystem)
    {
        skillPoints = skillSystem.skillPoints;
    }
    public void ApplySkillData(PlayerSkillSystem skillSystem)
    {
        skillSystem.skillPoints = skillPoints;
    }
}