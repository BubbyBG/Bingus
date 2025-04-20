using UnityEngine;
public class TokenItem : MonoBehaviour
{
    // for items which affect player on collision
    void Start()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider player)
    {
        PlayerSkillSystem skillSystem = player.GetComponent<PlayerSkillSystem>();
        // if (skillSystem == null)
        // {
        //     Debug.Log("collider doesn't have PlayerSkillsystem");
        // }
        PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
        string itemName = gameObject.name;

        if (itemName.Contains("FirstAid"))
        {
            playerInfo.ChangeHealth(10);
        }
        else if (itemName.Contains("Book"))
        {
            skillSystem.IncreaseSkillPoints(1);
        }
        Destroy(gameObject);
    }
}