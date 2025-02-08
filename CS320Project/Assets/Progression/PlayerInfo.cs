using UnityEditor;
using UnityEngine;
public class PlayerInfo : MonoBehaviour
{
    public int maxRange = 100;
    public int healthThreshold = 100; // base starting health value
    public int currentHealth;
    public StatusBar healthBar;
    public int staminaThreshold = 50; // base starting stamina value. threshold which stamina refills to.
    public int currentStamina; // decreases with sprinting. refills with walking.
    public StatusBar staminaBar;
    public int damageThreshold = 20; // base starting melee damage output. changes when using different weapons with fixed damage amount.
    // public int currentDamage; // usually the same as damage threshold
    public StatusBar damageBar;

    void Start()
    {
        currentHealth = healthThreshold;
        healthBar.SetValueRange(maxRange);
        healthBar.SetStatus(healthThreshold);

        currentStamina = staminaThreshold;
        healthBar.SetValueRange(maxRange);
        staminaBar.SetStatus(staminaThreshold);

        // currentDamage = damageThreshold;
        healthBar.SetValueRange(maxRange);
        damageBar.SetStatus(damageThreshold);
    }

    void Update()  // status change tests
    {
        if (Input.GetKeyDown(KeyCode.Q)) //take damage test
        {
            ChangeHealth(-20);
        }
        if (Input.GetKeyDown(KeyCode.W)) //heal test
        {
            ChangeHealth(20);
        }
        if (Input.GetKeyDown(KeyCode.E)) //lose stamina test
        {
            ChangeStamina(-10);
        }
        if (Input.GetKeyDown(KeyCode.R)) //regain stamina test
        {
            ChangeStamina(10);
        }
        if (Input.GetKeyDown(KeyCode.T)) // increase stamina threshold test
        {
            ChangeStaminaThreshold(70);
        }
        if (Input.GetKeyDown(KeyCode.Y)) // increase damage threshold test
        {
            ChangeDamageThreshold(90);
        }
        // if (Input.GetKeyDown(KeyCode.U)) //decrease damage output test
        // {
        //     ChangeDamageOutput(-5);
        // }
        // if (Input.GetKeyDown(KeyCode.I)) //increase damage output test
        // {
        //     ChangeDamageOutput(5);
        // }
    }

    public void ChangeHealth(int health) // used when taking damage, using first aid kits, other effects.
    {
        currentHealth += health;
        if (currentHealth < 0)
        {
            currentHealth = 0; // player death game status
        }
        if (currentHealth > healthThreshold)
        {
            currentHealth = healthThreshold;
        }
        healthBar.SetStatus(currentHealth);
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

    // public void ChangeDamageOutput(int damageOutput) // not necessary. optional for various effects. changeDamageThreshold would be used more often with weapon changes.
    // {
    //     currentDamage += damageOutput;
    //     if (currentDamage < 0)
    //     {
    //         currentDamage = 0;
    //     }
    //     if (currentDamage > damageThreshold)
    //     {
    //         currentDamage = damageThreshold;
    //     }
    //     damageBar.SetStatus(currentDamage);
    // }

    public void ChangeStaminaThreshold(int newStaminaThreshold) // non-incremental changes. used in player progression
    {
        staminaThreshold = newStaminaThreshold;
        if (staminaThreshold > maxRange)
        {
            staminaThreshold = maxRange;
        }
        if (staminaThreshold < 0) // stamina threshold wouldn't decrease in game
        {
            staminaThreshold = 0;
        }

    }

    public void ChangeDamageThreshold(int newDamageThreshold) // changes occur depending on what weapon the player is using. would need to be called in addition to 
    {
        damageThreshold = newDamageThreshold;
        if (damageThreshold > maxRange)
        {
            damageThreshold = maxRange;
        }
        if (damageThreshold < 20) //damageThreshold shouldn't decrease past start value
        {
            damageThreshold = 20;
        }
        damageBar.SetStatus(damageThreshold);
    }
}