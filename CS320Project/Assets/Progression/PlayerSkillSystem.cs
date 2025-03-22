using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// not finished
public class PlayerSkillSystem : MonoBehaviour
{
    public Text pointSystem;
    // private Text healthPoint; //incomplete
    // private Text staminaPoint;
    // private Text damagePoint;
    private PlayerInfo playerInfo;
    public int SkillPoints;

    public int EnemyDeaths;    //field to be updated from other scripts

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        SkillPoints = 5;
        pointSystem.text = "POINTS: " + SkillPoints;
    }

    void Update()
    {
        IncreaseSkillPoints();
        pointSystem.text = "POINTS: " + SkillPoints;

        if (Input.GetKeyDown(KeyCode.M))
        {
            skillToHealth();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            skillToStamina();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            skillToDamage();
        }
        if (Input.GetKeyDown(KeyCode.V)) //test
        {
            SkillPoints += 1;
        }
    }

    private void IncreaseSkillPoints()
    {
        if (EnemyDeaths >= 4) // example metric not final
        {
            SkillPoints += 1;
            EnemyDeaths -= 4;
        }
    }

    public void skillToHealth()
    {
        if (SkillPoints > 0)
        {
            SkillPoints -= 1;
            playerInfo.ChangeHealthThreshold(10);
        }
    }

    public void skillToStamina()
    {
        if (SkillPoints > 0)
        {
            SkillPoints -= 1;
            playerInfo.ChangeStaminaThreshold(10);
        }
    }

    public void skillToDamage()
    {
        if (SkillPoints > 0)
        {
            SkillPoints -= 1;
            playerInfo.ChangeDamageBonus(1);
        }
    }
}