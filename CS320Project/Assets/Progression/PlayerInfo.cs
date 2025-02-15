using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{
    public int maxRange = 100;
    public GameObject deathScreen;
    private CanvasGroup deathCanvasGroup;


    public int healthThreshold = 50; // base starting health value
    public int currentHealth;
    public StatusBar healthBar;


    public int staminaThreshold = 50; // base starting stamina value. threshold which stamina refills to.
    public int currentStamina; // decreases with sprinting. refills with walking.
    public StatusBar staminaBar;


    public int damageValue = 10;
    public int damageBonus = 0; // used for skill points
    public StatusBar damageBar;

    void Start()
    {
        deathCanvasGroup = deathScreen.GetComponent<CanvasGroup>();

        deathScreen.SetActive(false);

        currentHealth = healthThreshold;
        healthBar.SetValueRange(maxRange);
        healthBar.SetStatus(healthThreshold);

        currentStamina = staminaThreshold;
        healthBar.SetValueRange(maxRange);
        staminaBar.SetStatus(staminaThreshold);

        healthBar.SetValueRange(maxRange);
        damageBar.SetStatus(damageValue);
    }

    void Update()  // status change tests
    {
        if (Input.GetKeyDown(KeyCode.Q)) //take damage test
        {
            ChangeHealth(-10);
        }
        if (Input.GetKeyDown(KeyCode.W)) //heal test
        {
            ChangeHealth(10);
        }
        if (Input.GetKeyDown(KeyCode.E)) //lose stamina test
        {
            ChangeStamina(-10);
        }
        if (Input.GetKeyDown(KeyCode.R)) //regain stamina test
        {
            ChangeStamina(10);
        }
        if (Input.GetKeyDown(KeyCode.T)) //damge change test
        {
            ChangeDamageValue(20);
        }
        if (Input.GetKeyDown(KeyCode.Y)) //damage change test
        {
            ChangeDamageValue(90);
        }
    }

    public void ChangeHealth(int health) // used when taking damage, using first aid kits, other effects.
    {
        currentHealth += health;
        if (currentHealth <= 0)
        {
            currentHealth = 0; // player death game status
            PlayerDeath();
        }
        if (currentHealth > healthThreshold)
        {
            currentHealth = healthThreshold;
        }
        healthBar.SetStatus(currentHealth);
    }

    public void ChangeHealthThreshold(int healthThresholdIncrease)
    {
        if (healthThresholdIncrease > 0)
        {
            healthThreshold += healthThresholdIncrease;
            if (healthThreshold > maxRange)
            {
                healthThreshold = maxRange;
            }
        }
        else
        {
            Debug.Log("Don't decrease healthThreshold");
        }
    }

    public void ChangeStamina(int stamina) // alters current stamina and passes to statusbar. called inside sprinting function loop. called during melee attacks
    {
        currentStamina += stamina;
        if (currentStamina <= 0)
        {
            currentStamina = 0; // cancel sprinting and melee attacks. begin stamina refill
        }
        if (currentStamina > staminaThreshold)
        {
            currentStamina = staminaThreshold;
        }
        staminaBar.SetStatus(currentStamina);
    }

    public void ChangeStaminaThreshold(int staminaThresholdIncrease) // non-incremental changes. used in player progression
    {
        if (staminaThresholdIncrease > 0)
        {
            staminaThreshold += staminaThresholdIncrease;
            if (staminaThreshold > maxRange)
            {
                staminaThreshold = maxRange;
            }
        }
        else
        {
            Debug.Log("Don't decrease staminaThreshold");
        }
    }

    public void ChangeDamageValue(int newdamageValue) // changes occur depending on what weapon the player is using. damageBonus stays the same
    {
        damageValue = newdamageValue += damageBonus;
        if (damageValue > maxRange)
        {
            damageValue = maxRange;
        }
        if (damageValue < 20) //damageValue shouldn't decrease past start value
        {
            damageValue = 20;
        }
        damageBar.SetStatus(damageValue);
    }

    public void ChangeDamageBonus(int damageBonusIncrease) // used for damage skill point use
    {
        damageValue -= damageBonus; //revert to weapon base damage value
        damageBonus += damageBonusIncrease;
        ChangeDamageValue(damageValue); //update base weapon damage with new bonus
    }

    public void PlayerDeath()
    {
        deathScreen.SetActive(true);
        StartCoroutine(displayScreenLoadScene());
    }

    private IEnumerator displayScreenLoadScene()
    {
        float timeElapsed = 0f;
        float fadeDuration = 2f;
        while (timeElapsed < fadeDuration)
        {
            deathCanvasGroup.alpha = timeElapsed / fadeDuration;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        deathCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("StartMenuScene");
    }
}

