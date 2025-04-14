using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillSystem : MonoBehaviour
{
    public Text pointSystem;

    private string skillTextColor1 = "#990000"; // no points
    private string skillTextColor2 = "#3EA5F1"; // points
    public Color skillColor;

    public PlayerInfo playerInfo;
    public int SkillPoints;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        SkillPoints = 5;
        pointSystem.text = SkillPoints.ToString();
        changeSkillColor(skillTextColor2);
    }

    void Update()
    {
        pointSystem.text = SkillPoints.ToString();
        changeSkillColor(skillTextColor1);
        if (SkillPoints > 0)
        {
            changeSkillColor(skillTextColor2);
        }
        if (SkillPoints > 0)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SkillPoints -= 1;
                playerInfo.ChangeHealthThreshold(10);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                SkillPoints -= 1;
                playerInfo.ChangeStaminaThreshold(10);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                SkillPoints -= 1;
                playerInfo.ChangeDamageBonus(1);
            }
        }
    }

    private void changeSkillColor(string htmlColor)
    {
        ColorUtility.TryParseHtmlString(htmlColor, out skillColor);
        pointSystem.color = skillColor;
    }

    public void IncreaseSkillPoints(int points)
    {
        SkillPoints += points;
    }
}