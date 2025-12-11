using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public Animator animator;                // drag Animator here in Inspector

    [Header("Shooting")]
    public float range = 15f;
    public float fireInterval = 1.2f;
    public float projectileSpeed = 25f;
    public float projectileDamage = 15f;
    public float projectileLifetime = 3f;

    [Header("Movement (optional)")]
    public bool chasePlayer = false;
    public float chaseSpeed = 3f;
    public float stopDistance = 6f;

    Transform player;
    float cooldown = 0f;
    GameObject lastProjectile;

    // NEW: track last position to compute speed
    Vector3 lastPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (firePoint == null) firePoint = transform;

        lastPosition = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // face player horizontally
        Vector3 look = player.position - transform.position;
        look.y = 0;
        if (look.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(look);

        // movement
        if (chasePlayer && dist > stopDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * chaseSpeed * Time.deltaTime;
        }

        // === ANIMATION: set IsRunning based on actual movement speed ===
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        bool isRunning = speed > 0.05f;   // small threshold so tiny jitter doesn't count

        if (animator != null)
        {
            animator.SetBool("IsRunning", isRunning);
        }

        // shooting
        if (dist <= range)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                ShootAt(player.position);
                cooldown = fireInterval;
            }
        }

        // update last position at end of frame
        lastPosition = transform.position;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        if (chasePlayer)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, stopDistance);
        }
    }

    void OnDestroy()
    {
        if (lastProjectile != null)
        {
            Destroy(lastProjectile);
            lastProjectile = null;
        }
    }
}
