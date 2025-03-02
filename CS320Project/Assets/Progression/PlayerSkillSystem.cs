using UnityEditor;
using UnityEngine;
using TMPro;
using System.Collections;

// not finished
public class PlayerSkillSystem : MonoBehaviour
{
    public TMP_Text pointSystem;
    public TMP_Text healthPoint; //incomplete
    public TMP_Text staminaPoint;
    public TMP_Text damagePoint;
    private PlayerInfo playerInfo;
    private int SkillPoints;

    public int EnemyDeaths;    //field to be updated from other scripts

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        SkillPoints = 5;
        pointSystem.text = "POINTS: " + SkillPoints;
    }

    void Update()
    {
        if (EnemyDeaths >= 4) // example metric not final
        {
            SkillPoints += 1;
            EnemyDeaths -= 4;
        }
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