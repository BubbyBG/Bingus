using UnityEditor;
using UnityEngine;
// not finished
public class PlayerSkillSystem : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private int SkillPoints;

    private int PlayerDeaths;
    private int EnemyDeaths;
    private float RoomsExplored;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        SkillPoints = 5;
    }

    void Update() // Tests
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SkillPoints += 1;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            skillToHealth();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            skillToStamina();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            skillToDamage();
        }
    }

    public void skillToHealth()
    {
        SkillPoints -= 1;
        playerInfo.ChangeHealthThreshold(10);
    }

    public void skillToStamina()
    {
        SkillPoints -= 1;
        playerInfo.ChangeStaminaThreshold(10);
    }

    public void skillToDamage()
    {
        SkillPoints -= 1;
        playerInfo.ChangeDamageBonus(2);
    }
}