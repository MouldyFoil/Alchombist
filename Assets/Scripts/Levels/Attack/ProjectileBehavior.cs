using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [Header("Basic things")]
    [SerializeField] int damage = 1;
    [SerializeField] float speed = 5;
    [Header("Duration values")]
    [SerializeField] bool infiniteDuration = false;
    [SerializeField] float duration = 5;
    [Header("Acceleration values below")]
    [Header("hasAcceleration turns speed into max speed")]
    [SerializeField] bool hasAcceleration = false;
    [SerializeField] float acceleration;
    [SerializeField] float maxSidewaysSpeed = 2;
    [Header("Piercing projectiles")]
    [SerializeField] bool persistThroughBeings = false;
    [SerializeField] float hitCooldown = 0.5f;
    [Header("Audio")]
    [SerializeField] float volume = 1;
    [SerializeField] AudioClip[] spawnSounds;
    [SerializeField] AudioClip[] hitSounds;
    [Header("Misc")]
    [SerializeField] bool isPlayerProjectile = false;
    [SerializeField] bool isHoming = false;
    [Header("This is for if the projectile itself is not rotating")]
    [SerializeField] GameObject objectHomingOverride;
    SFXManager soundManager;
    Rigidbody2D rb;
    Vector3 target;
    Vector3 aimDirection;
    PlayerAimMain playerMainAim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        soundManager = FindObjectOfType<SFXManager>();
        if (isPlayerProjectile)
        {
            playerMainAim = FindObjectOfType<PlayerAimMain>();
            target = FindObjectOfType<PlayerAimMain>().marker.position;
        }
        if(spawnSounds != null && soundManager != null)
        {
            soundManager.PlayRandomAudioClip(spawnSounds, transform, volume);
        }
        if(objectHomingOverride == null)
        {
            objectHomingOverride = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!infiniteDuration)
        {
            duration -= Time.deltaTime;
        }
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
        HandleProjectileSpeed();
        if (isPlayerProjectile)
        {
            target = playerMainAim.marker.position;
        }
        if (isHoming == true && target != null)
        {
            HomeIn();
        }
    }

    private void HandleProjectileSpeed()
    {
        if (!hasAcceleration)
        {
            rb.velocity = objectHomingOverride.transform.up * speed;
        }
        else if (Vector2.Dot(rb.velocity, aimDirection) < speed)
        {
            rb.velocity += new Vector2(objectHomingOverride.transform.up.x, objectHomingOverride.transform.up.y) * acceleration * Time.deltaTime;
            if (Vector2.Dot(rb.velocity, objectHomingOverride.transform.right) > maxSidewaysSpeed)
            {
                rb.velocity -= new Vector2(objectHomingOverride.transform.right.x, objectHomingOverride.transform.right.y) * acceleration * Time.deltaTime;
            }
            if (Vector2.Dot(rb.velocity, -objectHomingOverride.transform.right) > maxSidewaysSpeed)
            {
                rb.velocity += new Vector2(objectHomingOverride.transform.right.x, objectHomingOverride.transform.right.y) * acceleration * Time.deltaTime;
            }
        }
    }

    public void AddExtraDamage(int extraDamage)
    {
        damage += extraDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerProjectile && FindObjectOfType<CriticalPotion>())
        {
            float damageMultiplied = damage;
            foreach(CriticalPotion criticalPotion in FindObjectsOfType<CriticalPotion>())
            {
                damageMultiplied *= criticalPotion.RollForMultiplier();
            }
            damage = (int)damageMultiplied;
        }
        if (collision.GetComponent<Health>())
        {
            if(persistThroughBeings == false)
            {
                Destroy(gameObject);
            }
            collision.GetComponent<Health>().AddOrRemoveGeneralHealth(-damage);
        }
        else
        {
            Destroy(gameObject);
        }
        if (hitSounds.Count() > 0 && soundManager != null)
        {
            soundManager.PlayRandomAudioClip(hitSounds, transform, volume);
        }
    }
    private void HomeIn()
    {
        target.z = objectHomingOverride.transform.position.z;
        aimDirection = (target - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        objectHomingOverride.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    //for animations (if i need probably not now that i think about it)
    //public void ChangeSpeed(float newSpeed, float transitionSpeed)
    //{
    //    speed = Mathf.Lerp(speed, newSpeed, transitionSpeed);
    //}
}
