using Assets.Scripts.Interfaces;
using UnityEngine;

public class RangedEnemy : MonoBehaviour, IAttackable
{
    [Header("References")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public Animator animator;                // drag Animator here in Inspector

    [Header("Shooting")]
    public float range = 15f;
    public float fireInterval = 3f;
    public float projectileSpeed = 25f;
    public float projectileDamage = 15f;
    public float projectileLifetime = 3f;

    [Header("Movement (optional)")]
    public bool chasePlayer = false;
    public float chaseSpeed = 3f;
    public float stopDistance = 6f;

    [Header("Stats")]
    [SerializeField] private float currentHealth = 70f;

    Transform player;
    float cooldown = 0f;
    GameObject lastProjectile;

    // NEW: track last position to compute speed
    Vector3 lastPosition;
    public EnemyMovement movement;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (firePoint == null) firePoint = transform;


    }

    void Update()
    {
        if (animator != null)
            animator.Play("Slow Run");
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // face player horizontally
        // Vector3 look = player.position - transform.position;
        // look.y = 0;
        // if (look.sqrMagnitude > 0.001f)
        //     transform.rotation = Quaternion.LookRotation(look);

        // shooting
        if (movement.ChasingMove && dist <= range)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                ShootAt(player.position);
                cooldown = fireInterval;
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        IAttackable gotHit = other.GetComponent<IAttackable>();

        if (gotHit != null)
            gotHit.TakeDamage(projectileDamage);

    }

    void ShootAt(Vector3 targetPos)
    {
        Vector3 spawnPos = firePoint.position;
        Vector3 dir = (targetPos - spawnPos).normalized;

        if (lastProjectile != null)
        {
            Destroy(lastProjectile);
            lastProjectile = null;
        }

        if (projectilePrefab == null)
        {
            Debug.LogError("RangedEnemy: projectilePrefab not assigned.");
            return;
        }

        GameObject projGO = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(dir));
        projGO.transform.position = spawnPos;
        projGO.transform.rotation = Quaternion.LookRotation(dir);

        var proj = projGO.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Init(dir, projectileSpeed, projectileDamage, projectileLifetime);
        }

        lastProjectile = projGO;
    }

    void OnDestroy()
    {
        if (lastProjectile != null)
        {
            Destroy(lastProjectile);
            lastProjectile = null;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
            Destroy(gameObject);
    }
}
