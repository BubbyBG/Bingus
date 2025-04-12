using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    //Unity References
    private GameObject player;
    private Inventory inventory;
    private PlayerInput input;
    private Animator _animation;
    [SerializeField]
    private AnimationClip anim_recoil;
    [SerializeField]
    private AnimationClip anim_equip;
    private AudioSource audioS;

    //Gun stats
    public GameObject bulletType;
    public GameObject fireEffect;
    public float weaponCooldown;
    //public Sound soundFire;
    //public Sound soundEquip;

    
    public enum weaponType
    {
        melee,
        gun,
    }
    private float cooldownTimer;
    [SerializeField]
    private bool canUse; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canUse = true;
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        input = player.GetComponent<PlayerInput>();
        _animation = GetComponent<Animator>();
       
        //_animation = transform.GetChild(0).GetComponent<Animation>();
        //_animation.clip = anim_recoil;
        cooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canUse)
        {
            cooldownTimer -= 120f * Time.deltaTime;
        }
        else
        {
            cooldownTimer = 0f;
        }

        if (cooldownTimer < 0)
        {
            canUse = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _animation.Play("Fire");
        }
    }

    public void UseWeaponPrimary()
    {
        if (canUse)
        {
            Vector3 muzzle = transform.GetChild(1).position;
            GameObject projectile = Instantiate(bulletType, muzzle, transform.rotation);
            projectile.GetComponent<ProjectileClass>().Shoot(input.posFrom, input.dirTo);
            //_animation.Play("gun_model|Recoil");
            _animation.Play("Fire");

            FindAnyObjectByType<AudioManager>().Play("pistol");

            GameObject muzzleFlash = Instantiate(fireEffect, muzzle, transform.rotation);
            //audioS.Play(soundFire);
            cooldownTimer = weaponCooldown;
            canUse = false;
        }
    }

    public void Equip()
    {

    }
}
