using System.Collections;
using UnityEngine;
using Assets.Scripts.Abstract;
using Assets.Scripts.Combat;
using Assets.Scripts.Interfaces;

public class BossController : MonoBehaviour
{
    [Header("References")]
    public GameObject projectilePrefab;
    public Transform projectilesParent;
    public Transform player;

    [Header("Projectile Settings")]
    public float spawnRadius = 10f;
    public float spawnInterval = 1f;
    public float projectileSpeed = 4f;
    public int maxSimultaneous = 5;

    [Header("Player Detection")]
    public float detectionRange = 12f;
    [SerializeField] float damageAmount = 10f;


    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            // Only attack if player is close
            if (player != null)
            {
                float dist = Vector3.Distance(transform.position, player.position);
                if (dist <= detectionRange)
                {
                    TrySpawnProjectile();
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void TrySpawnProjectile()
    {
        if (maxSimultaneous > 0 && projectilesParent.childCount >= maxSimultaneous)
            return;

        // Pick random point around boss
        Vector2 rand = Random.insideUnitCircle * spawnRadius;

        Vector3 target = new Vector3(
            transform.position.x + rand.x,
            transform.position.y,
            transform.position.z + rand.y
        );

        // Spawn projectile
        GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectilesParent);

        // Use the generic Initialize method
        FireProjectile proj = go.GetComponent<FireProjectile>();
        proj.Initialize(target, projectileSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Collided with Enemy");

        IAttackable dmg = other.GetComponent<IAttackable>();
        // if projet hit an attackabale object -> [player]
        if (dmg != null)
            dmg.TakeDamage(damageAmount);
    }

    private void OnDrawGizmosSelected()
    {
        // Projectile radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);

        // Player detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
