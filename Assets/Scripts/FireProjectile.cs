using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    private Vector3 target;
    private float speed = 5f;
    private bool initialized = false;

    [Header("Lifecycle")]
    public float arrivalThreshold = 0.05f;  // how close to consider 'arrived'
    public float stayDuration = 1.0f;       // seconds to stay at target before destroy
    public bool faceMovementDirection = false;

    // Call this immediately after Instantiate
    public void SetTarget(Vector3 targetPosition, float moveSpeed)
    {
        target = targetPosition;
        speed = moveSpeed;
        initialized = true;
    }

    void Update()
    {
        if (!initialized)
            return; // wait until boss sets target

        // Move toward target on every frame
        Vector3 dir = target - transform.position;
        // Keep y fixed if you want it to stay on same height
        dir.y = 0f;

        if (dir.magnitude <= arrivalThreshold)
        {
            // Arrived: start the arrival coroutine and disable further movement
            initialized = false;
            StartCoroutine(OnArrivalAndDestroy());
            return;
        }

        Vector3 move = dir.normalized * speed * Time.deltaTime;
        // Avoid overshoot
        if (move.magnitude > dir.magnitude) move = dir;
        transform.position = Vector3.Lerp(
            transform.position,
            target,
            speed * Time.deltaTime
            );

        if (faceMovementDirection && move != Vector3.zero)
            transform.forward = move.normalized;
    }

    IEnumerator OnArrivalAndDestroy()
    {
        // Snap exactly to target (preserve y of transform or set to target.y)
        transform.position = new Vector3(target.x, transform.position.y, target.z);

        // Optional: play particle/effect or change color here
        // e.g. GetComponent<Renderer>().material.color = Color.yellow;

        yield return new WaitForSeconds(stayDuration);

        // destory object after stayDuration time ends 
        Destroy(gameObject);
    }
}
