using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;              // where bullets spawn
    public GameObject projectilePrefab;      // projectile prefab (required)

    [Header("Shooting")]
    public float range = 15f;
    public float fireInterval = 1.2f;        // seconds between shots
    public float projectileSpeed = 25f;
    public float projectileDamage = 15f;
    public float projectileLifetime = 3f;

    [Header("Movement (optional)")]
    public bool chasePlayer = false;
    public float chaseSpeed = 3f;
    public float stopDistance = 6f;

    Transform player;
    float cooldown = 0f;

    // --- NEW: track the last projectile spawned by this enemy ---
    GameObject lastProjectile;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (firePoint == null) firePoint = transform;
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

        if (chasePlayer && dist > stopDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * chaseSpeed * Time.deltaTime;
        }

        if (dist <= range)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                ShootAt(player.position);
                cooldown = fireInterval;
            }
        }
    }

    void ShootAt(Vector3 targetPos)
    {
        Vector3 spawnPos = firePoint.position;
        Vector3 dir = (targetPos - spawnPos).normalized;

        // --- DESTROY previous projectile if it still exists ---
        if (lastProjectile != null)
        {
            // Destroy previous immediately (will actually be destroyed at end of frame)
            Destroy(lastProjectile);
            lastProjectile = null;
        }

        // Spawn new projectile (simple Instantiate path - expected when using destroy pattern)
        if (projectilePrefab == null)
        {
            Debug.LogError("RangedEnemy: projectilePrefab not assigned.");
            return;
        }

        GameObject projGO = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(dir));
        // ensure the object is positioned/rotated correctly
        projGO.transform.position = spawnPos;
        projGO.transform.rotation = Quaternion.LookRotation(dir);

        // init projectile if it has Projectile component
        var proj = projGO.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Init(dir, projectileSpeed, projectileDamage, projectileLifetime);
        }

        // keep reference so next shot will destroy this one
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

    // Optional: clean up if enemy is destroyed (destroy any leftover projectile it owned)
    void OnDestroy()
    {
        if (lastProjectile != null)
        {
            Destroy(lastProjectile);
            lastProjectile = null;
        }
    }
}
