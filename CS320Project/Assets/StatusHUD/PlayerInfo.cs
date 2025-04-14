using UnityEngine;
using System.Collections;
// using UnityEngine.Assertions;

public class PlayerInfo : MonoBehaviour
{
    public int maxRange = 100;
    public DeathScreen deathScreen;

    public int healthThreshold = 50; // base starting health value
    public int currentHealth;
    public StatusBar healthBar;
    private string currentHealthColor;
    private string healthColor1 = "#0E940F"; // full
    private string healthColor2 = "#FFDE0F";
    private string healthColor3 = "#E47413";
    private string healthColor4 = "#B7120A"; // low

    public int staminaThreshold = 50; // base starting stamina value. threshold which stamina refills to.
    public int currentStamina; // decreases with sprinting. refills with walking.
    public StatusBar staminaBar;
    private string staminaColor1 = "#CEC400";

    public int damageValue = 5;
    public int damageBonus = 0; // used for skill points
    public StatusBar damageBar;
    private string damageColor1 = "#C332CD";

    public Coroutine SprintRoutine;
    public Coroutine StaminaRefillRoutine;

    void Start()
    {
        // healthBar = new GameObject("HealthBar").AddComponent<StatusBar>(); //for testing
        // staminaBar = new GameObject("StaminaBar").AddComponent<StatusBar>(); //for testing
        // damageBar = new GameObject("DamageBar").AddComponent<StatusBar>(); //for testing

        currentHealth = healthThreshold;
        healthBar.SetValueRange(maxRange);
        healthBar.SetStatus(currentHealth, healthThreshold, healthColor1);

        currentStamina = staminaThreshold;
        healthBar.SetValueRange(maxRange);
        staminaBar.SetStatus(currentStamina, staminaThreshold, staminaColor1);
        SprintRoutine = null;
        StaminaRefillRoutine = null;

        healthBar.SetValueRange(maxRange);
        damageBar.SetStatus(damageValue, damageValue, damageColor1);
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
        if (Input.GetKeyDown(KeyCode.R)) //damge change test
        {
            ChangeDamageValue(20);
        }
        if (Input.GetKeyDown(KeyCode.T)) //damage change test
        {
            ChangeDamageValue(90);
        }
        if (Input.GetKeyDown(KeyCode.Y)) //sprint test
        {
            StartStopSprint(-1, 0.1f);
        }
        if ((SprintRoutine == null) && (StaminaRefillRoutine == null) && (currentStamina < staminaThreshold))
        {
            StartStaminaRefill(1, 0.2f);
        }
    }


    public void ChangeHealth(int health) // used when taking damage, using first aid kits, other effects.
    {
        currentHealthColor = healthColor1;
        currentHealth += health;
        if (currentHealth <= 0)
        {
            currentHealth = 0; // player death game status
            deathScreen.activate();
        }
        if (currentHealth > healthThreshold)
        {
            currentHealth = healthThreshold;
        }
        if (currentHealth > healthThreshold * 0.75)
        {
            currentHealthColor = healthColor1;
        }
        else if (currentHealth > healthThreshold * 0.5)
        {
            currentHealthColor = healthColor2;
        }
        else if (currentHealth > healthThreshold * 0.25)
        {
            currentHealthColor = healthColor3;
        }
        else
        {
            currentHealthColor = healthColor4;
        }
        healthBar.SetStatus(currentHealth, healthThreshold, currentHealthColor);
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
            ChangeHealth(0);
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
        staminaBar.SetStatus(currentStamina, staminaThreshold, staminaColor1);
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
            staminaBar.SetStatus(currentStamina, staminaThreshold, staminaColor1);
        }
    }

    public void StartStopSprint(int change, float rate)
    {
        if (SprintRoutine != null)
        {
            StopAllCoroutines();
            SprintRoutine = null;
        }
        else
        {
            if (StaminaRefillRoutine != null)
            {
                StopCoroutine(StaminaRefillRoutine);
                StaminaRefillRoutine = null;
            }
            SprintRoutine = StartCoroutine(SprintingStamina(change, rate));
        }
    }

    IEnumerator SprintingStamina(int change, float rate)
    {
        while ((currentStamina > 0))
        {
            ChangeStamina(change);
            yield return new WaitForSeconds(rate);
        }
        SprintRoutine = null;
        yield return null;
    }

    public void StartStaminaRefill(int change, float rate)
    {
        if (StaminaRefillRoutine != null)
        {
            StopCoroutine(StaminaRefillRoutine);
            StaminaRefillRoutine = null;
        }
        else
        {
            if (SprintRoutine != null)
            {
                StopCoroutine(SprintRoutine);
                SprintRoutine = null;
            }
            StaminaRefillRoutine = StartCoroutine(StaminaRefill(change, rate));
        }
    }

    IEnumerator StaminaRefill(int change, float rate)
    {
        if (currentStamina == 0)
        { // penalize player if stamina gets too low
            yield return new WaitForSeconds(3); // calling sprint while penalty wait is occurring will cause error
        }
        while ((currentStamina < staminaThreshold))
        {
            ChangeStamina(change);
            yield return new WaitForSeconds(rate);
        }
        StaminaRefillRoutine = null;
        yield return null;
    }

    public void ChangeDamageValue(int newdamageValue) // changes occur depending on what weapon the player is using. damageBonus stays the same
    {
        damageValue = newdamageValue += damageBonus;
        if (damageValue > maxRange)
        {
            damageValue = maxRange;
        }
        if (damageValue < 5) //damageValue shouldn't decrease past start value
        {
            damageValue = 5;
        }
        damageBar.SetStatus(damageValue - damageBonus, damageValue, damageColor1);
    }
    public void ChangeDamageBonus(int damageBonusIncrease) // used for damage skill point use
    {
        damageValue -= damageBonus; //revert to weapon base damage value
        damageBonus += damageBonusIncrease;
        ChangeDamageValue(damageValue); //update base weapon damage with new bonus
    }
}