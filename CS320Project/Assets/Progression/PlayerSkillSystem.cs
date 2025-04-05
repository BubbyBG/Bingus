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

    private string skillTextColor1 = "#990000"; // no points
    private string skillTextColor2 = "#60F0D9"; // points
    public Color skillColor;

    private PlayerInfo playerInfo;
    public int SkillPoints;
    public int EnemyDeaths;    //field to be updated from other scripts

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        SkillPoints = 5;
        pointSystem.text = "  " + SkillPoints;
        changeSkillColor(skillTextColor2);
    }

    void Update()
    {
        IncreaseSkillPoints();
        pointSystem.text = "  " + SkillPoints;
        if (SkillPoints > 0)
        {
            changeSkillColor(skillTextColor2);
        }
        else
        {
            changeSkillColor(skillTextColor1);
        }

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

    private void changeSkillColor(string htmlColor)
    {
        ColorUtility.TryParseHtmlString(htmlColor, out skillColor);
        pointSystem.color = skillColor;
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