using UnityEngine;
public class TokenItem : MonoBehaviour
{
    // for items which affect player on collision
    private void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider player)
    {
        PlayerSkillSystem skillSystem = player.GetComponent<PlayerSkillSystem>();
        PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
        string itemName = gameObject.name;

        if (itemName == "FirstAid")
        {
            playerInfo.ChangeHealth(10);
        }
        else if (itemName == "Book")
        {
            skillSystem.IncreaseSkillPoints();
        }
        Destroy(gameObject);
    }
}