using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject firePrefab;           // assign FirePrefab here
    public Transform projectilesParent;     // optional parent for instantiated projectiles
    public float spawnInterval = 1.0f;      // seconds between spawns
    public int maxSimultaneous = 5;         // how many can exist at once

    [Header("Targeting")]
    public float spawnRadius = 5f;          // radius around boss where fires land
    public float projectileSpeed = 4f;      // movement speed of fire

    [Header("Player Detection")]
    public Transform player;         // assign the Player cube here
    public float detectionRange = 10f;   // boss activates when player is inside this range


    void Start()
    {
        if (firePrefab == null)
        {
            Debug.LogError("Fire prefab is not assigned on BossController.");
            enabled = false;
            return;
        }
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // 1) Check distance to player
            if (player != null)
            {
                float dist = Vector3.Distance(transform.position, player.position);

                // If player is OUTSIDE range → do nothing this frame
                if (dist > detectionRange)
                {
                    yield return null;
                    continue;
                }
            }

            // 2) Only spawn if player is in range
            if (maxSimultaneous <= 0 || CountProjectiles() < maxSimultaneous)
            {
                SpawnFire();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    int CountProjectiles()
    {
        if (projectilesParent != null)
            return projectilesParent.childCount;
        // fallback: count all objects with tag "Projectile" if you set tag
        return 0;
    }

    void SpawnFire()
    {
        // 1) Calculate a random target position on XZ plane around boss
        Vector2 rand = Random.insideUnitCircle * spawnRadius;
        Vector3 target = new Vector3(transform.position.x + spawnRadius, transform.position.y, transform.position.z + rand.y);

        // 2) Instantiate fire at boss position
        // spawn position for the fire obj
        Vector3 spawnPos = transform.position + new Vector3(0, -1.0f, 0); ;
        GameObject go = Instantiate(firePrefab, spawnPos, Quaternion.identity, projectilesParent);

        // 3) Configure the projectile (pass target and speed)
        FireProjectile fp = go.GetComponent<FireProjectile>();
        if (fp != null)
        {
            fp.SetTarget(target, projectileSpeed);
        }
        else
        {
            // If the prefab doesn't have the script, move it manually
            go.transform.position = Vector3.MoveTowards(spawnPos, target, 0.01f);
        }
    }

    // Visualize spawn radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
