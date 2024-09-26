using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float duration = 5;
    [SerializeField] float speed = 5;
    [SerializeField] bool infiniteDuration = false;
    [SerializeField] bool isHoming = false;
    [SerializeField] bool persistThroughBeings = false;
    Rigidbody2D rb;
    GameObject spawner;
    Vector3 target;
    Vector3 aimDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(infiniteDuration == false)
        {
            StartCoroutine(LifeSpan());
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
        if(isHoming == true && target != null)
        {
            UpdateTarget();
            HomeIn();
        }
    }
    public void AddExtraDamage(int extraDamage)
    {
        damage += extraDamage;
    }
    public void SetSpawner(GameObject spawn)
    {
        spawner = spawn;
    }
    private IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>())
        {
            if(persistThroughBeings == false)
            {
                Destroy(gameObject);
            }
            collision.GetComponent<Health>().AddOrRemoveHealth(-damage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void UpdateTarget()
    {
        Transform[] enemies = GameObject.FindGameObjectsWithTag("TargetableByPlayer").Select(player => player.transform).ToArray();
        if (enemies.Length > 0)
        {
            target = GetClosestEnemy(enemies).position;
        }
        if (enemies.Length == 0)
        {
            target = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        }
    }
    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform transformMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.position, currentPos);
            if (dist < minDist)
            {
                transformMin = enemy;
                minDist = dist;
            }
        }
        return transformMin;
    }
    private void HomeIn()
    {
        target.z = transform.position.z;
        aimDirection = (target - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    //for animations (if i need probably not now that i think about it)
    //public void ChangeSpeed(float newSpeed, float transitionSpeed)
    //{
    //    speed = Mathf.Lerp(speed, newSpeed, transitionSpeed);
    //}
}
