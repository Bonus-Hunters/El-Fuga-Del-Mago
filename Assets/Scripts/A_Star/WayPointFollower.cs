using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] wayPoints;
    int currentWayPointIndex = 0;

    [SerializeField] float speed = 1f;

    // NEW: Choose if path should repeat
    public bool loopPath = true;

    public bool hasFinishedPath = false;
    public bool isWalking = true;                // whether movement is allowed
    bool wasMovingLastFrame = false;      // to detect "stop"
    float stopThreshold = 0.0001f;        // speed threshold
    Vector3 lastPos;                      // to detect movement

    void Start()
    {
        lastPos = transform.position;
    }
    void Update()
    {
        // If path is done and looping is disabled, stop moving
        if (hasFinishedPath || !isWalking)
            return;

        float fixedY = transform.position.y;

        // Get next waypoint
        Vector3 target = wayPoints[currentWayPointIndex].transform.position;
        target.y = fixedY;

        // Direction toward target
        Vector3 direction = (target - transform.position).normalized;

        // Rotate to face movement direction (Y-only)
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        // Move toward waypoint
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if reached
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentWayPointIndex++;

            // Reached the end of the path?
            if (currentWayPointIndex >= wayPoints.Length)
            {
                if (loopPath)
                {
                    currentWayPointIndex = 0;  // restart
                }
                else
                {
                    hasFinishedPath = true;   // stop forever
                    return;
                }
            }
        }
    }
    // Interrupt walking at runtime
    public void StopWalking()
    {
        isWalking = false;
        wasMovingLastFrame = false;
    }

    // Resume walking from where we stopped
    public void ResumeWalking()
    {
        isWalking = true;
        lastPos = transform.position; // reset movement tracking
    }
}
