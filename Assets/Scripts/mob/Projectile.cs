using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [Header("Flight")]
    public float speed = 20f;
    public float damage = 10f;
    [Tooltip("Seconds before the projectile is destroyed automatically")]
    public float lifeTime = 1f;

    Vector3 direction;
    Coroutine autoDestroyCoroutine;
    Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true; // projectile expects trigger collisions
    }

    public void Init(Vector3 dir, float spd, float dmg, float lifetime)
    {
        direction = dir.normalized;
        speed = spd;
        damage = dmg;
        lifeTime = lifetime;

        // start / restart auto destroy
        if (autoDestroyCoroutine != null) StopCoroutine(autoDestroyCoroutine);
        autoDestroyCoroutine = StartCoroutine(AutoDestroyRoutine(lifeTime));
    }

    void OnEnable()
    {
        // safety: ensure we always destroy eventually even if Init wasn't called
        if (autoDestroyCoroutine == null)
            autoDestroyCoroutine = StartCoroutine(AutoDestroyRoutine(lifeTime));
    }

    void OnDisable()
    {
        if (autoDestroyCoroutine != null)
        {
            StopCoroutine(autoDestroyCoroutine);
            autoDestroyCoroutine = null;
        }
    }

    void Update()
    {
        if (direction.sqrMagnitude > 0.0001f && speed != 0f)
            transform.position += direction * speed * Time.deltaTime;
    }

    IEnumerator AutoDestroyRoutine(float secs)
    {
        yield return new WaitForSeconds(secs);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // ignore the shooter/enemies if needed (optional)
        if (other.CompareTag("Enemy")) return;

        if (other.CompareTag("Player"))
        {
            IAttackable gotHit = other.GetComponent<IAttackable>();

            if (gotHit != null)
                gotHit.TakeDamage(damage);

        }

        // Prevent further collisions while being destroyed
        if (col != null) col.enabled = false;

        // stop coroutine to avoid double calls (not strictly necessary)
        if (autoDestroyCoroutine != null)
        {
            StopCoroutine(autoDestroyCoroutine);
            autoDestroyCoroutine = null;
        }

        Destroy(gameObject);
    }
}
