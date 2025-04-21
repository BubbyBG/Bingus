using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillSystem : MonoBehaviour
{
    public Text pointSystem;
    private string skillTextColor1 = "#990000"; // no points
    private string skillTextColor2 = "#3EA5F1"; // points
    public Color skillColor;

    public PlayerInfo playerInfo;
    public int skillPoints;

    void Start()
    {
        if (!SaveManager.isLoadGame) // new game
        {
            skillPoints = 0;
        }
        playerInfo = GetComponent<PlayerInfo>();
        pointSystem.text = skillPoints.ToString();
        changeSkillColor(skillTextColor2);
    }

    void Update()
    {
        pointSystem.text = skillPoints.ToString();
        changeSkillColor(skillTextColor1);
        if (skillPoints > 0)
        {
            changeSkillColor(skillTextColor2);
        }
        if (skillPoints > 0)
        {
            bool shiftHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            if (shiftHeld)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    skillPoints -= 1;
                    playerInfo.ChangeHealthThreshold(10);
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    skillPoints -= 1;
                    playerInfo.ChangeStaminaThreshold(10);
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    skillPoints -= 1;
                    playerInfo.ChangeDamageBonus(1);
                }
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
        skillPoints += points;
    }
}