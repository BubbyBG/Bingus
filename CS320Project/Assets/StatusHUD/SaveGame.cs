using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public PlayerSkillSystem skillSystem;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        skillSystem = GetComponent<PlayerSkillSystem>();

        if (SaveManager.isLoadGame) // load preexisting
        {
            SaveManager.LoadGameSave(playerInfo, skillSystem);
        }
        else // start new
        {
            SaveManager.DeleteSaveData(); // race condition
            SaveManager.SaveGame(playerInfo, skillSystem);
        }
    }
    void Update() // save on player input 
    {
        bool shiftHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (shiftHeld)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SaveManager.SaveGame(playerInfo, skillSystem);
            }
        }
    }
    void OnApplicationQuit() // save when quitting
    {
        if (!playerInfo.playerDied)
        {
            SaveManager.SaveGame(playerInfo, skillSystem);
        }
    }
}