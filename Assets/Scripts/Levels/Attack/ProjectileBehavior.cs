using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
    [SerializeField] float acceleration = 5;
    [SerializeField] float deccelerationAtMax = 2;
    [SerializeField] float maxSidewaysSpeed = 2;
    [SerializeField] float sideSpeedCorrectionAcceleration = 50f;
    [Header("Piercing projectiles")]
    [SerializeField] string[] persistThroughTags;
    [SerializeField] float hitCooldown = 0.5f;
    [Header("Audio")]
    [SerializeField] float volume = 1;
    [SerializeField] AudioClip[] spawnSounds;
    [SerializeField] AudioClip[] hitSounds;
    [Header("Parry things")]
    [SerializeField] UnityEvent parryEvent;
    [SerializeField] int parryHeal = 1;
    public int parryType;
    [Header("Misc")]
    [SerializeField] bool isPlayerProjectile = false;
    [SerializeField] bool isHoming = false;
    [SerializeField] UnityEvent destroyEvent;
    [SerializeField] UnityEvent hitEnemyEvent;
    [Header("This is for if the projectile itself is not rotating")]
    [SerializeField] GameObject objectHomingOverride;
    float hitTimer;
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
        if(hitTimer > 0) { hitTimer -= Time.deltaTime; }
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
        else
        {
            if (Vector2.Dot(rb.velocity, objectHomingOverride.transform.up) <= speed)
            {
                rb.velocity += new Vector2(objectHomingOverride.transform.up.x, objectHomingOverride.transform.up.y) * acceleration * Time.deltaTime;
            }
            else
            {
                rb.velocity -= new Vector2(objectHomingOverride.transform.up.x, objectHomingOverride.transform.up.y) * deccelerationAtMax * Time.deltaTime;
            }
            if (Vector2.Dot(rb.velocity, objectHomingOverride.transform.right) > maxSidewaysSpeed)
            {
                rb.velocity -= new Vector2(objectHomingOverride.transform.right.x, objectHomingOverride.transform.right.y) * sideSpeedCorrectionAcceleration * Time.deltaTime;
            }
            if (Vector2.Dot(rb.velocity, -objectHomingOverride.transform.right) > maxSidewaysSpeed)
            {
                rb.velocity += new Vector2(objectHomingOverride.transform.right.x, objectHomingOverride.transform.right.y) * sideSpeedCorrectionAcceleration * Time.deltaTime;
            }
        }
    }

    public void AddExtraDamage(int extraDamage)
    {
        damage += extraDamage;
    }
    public void AddTime(float amount)
    {
        duration += amount;
    }
    public void AddSpeed(float increase)
    {
        speed += increase;
    }
    public void AddAcceleration(float increase)
    {
        acceleration += increase;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParryScript parryScript = null;
        if (collision.GetComponentInChildren<ParryScript>())
        {
            parryScript = collision.GetComponentInChildren<ParryScript>();
        }
        if (parryScript != null && parryScript.ReturnParryActive() && parryScript.ReturnParryType() == parryType)
        {
            collision.GetComponent<Health>().AddOrRemoveGeneralHealth(parryHeal);
            rb.velocity = -rb.velocity;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 180);
            rb.velocity = -rb.velocity;
            parryScript.ParrySuccessFX();
            parryEvent.Invoke();
        }
        else
        {
            NonParryBehavior(collision);
        }
    }

    private void NonParryBehavior(Collider2D collision)
    {
        if (isPlayerProjectile && FindObjectOfType<CriticalPotion>())
        {
            float damageMultiplied = damage;
            foreach (CriticalPotion criticalPotion in FindObjectsOfType<CriticalPotion>())
            {
                damageMultiplied *= criticalPotion.RollForMultiplier();
            }
            damage = (int)damageMultiplied;
        }
        if (collision.GetComponent<Health>())
        {
            if (DoesntPersist(collision.gameObject))
            {
                Destroy(gameObject);
            }
            if (hitTimer <= 0)
            {
                hitTimer = hitCooldown;
                collision.GetComponent<Health>().AddOrRemoveGeneralHealth(-damage);
                hitEnemyEvent.Invoke();
            }
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

    private bool DoesntPersist(GameObject collidingObject)
    {
        foreach (string tag in persistThroughTags)
        {
            if(collidingObject.tag == tag)
            {
                return false;
            }
        }
        return true;
    }
    private void HomeIn()
    {
        target.z = objectHomingOverride.transform.position.z;
        aimDirection = (target - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        objectHomingOverride.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void SetVelocity(Vector2 velocitySet)
    {
        rb.velocity = velocitySet;
    }
    private void OnDestroy()
    {
        destroyEvent.Invoke();
    }
    public void SetIsHoming(bool homing)
    {
        isHoming = homing;
        playerMainAim = FindObjectOfType<PlayerAimMain>();
        target = FindObjectOfType<PlayerAimMain>().marker.position;
    }
    public void SetIsPlayerProjectile(bool players) { isPlayerProjectile = players; }
    //for animations (if i need probably not now that i think about it)
    //public void ChangeSpeed(float newSpeed, float transitionSpeed)
    //{
    //    speed = Mathf.Lerp(speed, newSpeed, transitionSpeed);
    //}
}
