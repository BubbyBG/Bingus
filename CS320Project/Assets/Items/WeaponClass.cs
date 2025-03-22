using UnityEngine;

public class WeaponClass : ItemClass //inherits from ItemClass
{
    //Unity References
    private GameObject player;
    private Inventory inventory;
    private PlayerInput input;
    private bool active;

    //Gun stats
    public GameObject bulletType;
    public float weaponCooldown;
    public Animation weaponAnimation;
    public enum weaponType
    {
        melee,
        gun,
    }

    private float cooldownTimer;
    private bool canUse; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canUse = true;
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        input = player.GetComponent<PlayerInput>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canUse)
        {
            cooldownTimer -= 1f * Time.deltaTime;
        }
        else
        {
            cooldownTimer = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            if (canUse)
            {
                UseWeaponPrimary();
                cooldownTimer = weaponCooldown;
                canUse = false;
            }
        }
    }

    void UseWeaponPrimary()
    {
        Vector3 muzzle = transform.GetChild(1).position;
        GameObject projectile = Instantiate(bulletType, muzzle, transform.rotation);
        projectile.GetComponent<ProjectileClass>().Shoot(input.posFrom, input.dirTo);
    }

    public void Equip()
    {
        active = true;
        
    }
}
