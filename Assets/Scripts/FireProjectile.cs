using System.Collections;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    private Vector3 target;
    private float speed;
    private bool hasTarget = false;

    public float arrivalThreshold = 0.05f;
    public float stayDuration = 1f;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.15f; // controls how smooth the movement is

    // -------------------------
    // PUBLIC, GENERIC API
    // -------------------------
    public void Initialize(Vector3 targetPosition, float moveSpeed)
    {
        target = targetPosition;
        speed = moveSpeed;
        hasTarget = true;
    }

    void Update()
    {
        if (!hasTarget) return;

        // Smooth movement
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target,
            ref velocity,
            smoothTime,
            Mathf.Infinity,
            Time.deltaTime
        );

        // Check arrival
        if (Vector3.Distance(transform.position, target) <= arrivalThreshold)
        {
            hasTarget = false;
            StartCoroutine(HandleArrival());
        }
    }

    IEnumerator HandleArrival()
    {
        // Snap to exact position
        transform.position = target;

        // Wait then destroy
        yield return new WaitForSeconds(stayDuration);
        Destroy(gameObject);
    }
}
