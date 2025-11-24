using UnityEngine;
using System.Collections.Generic;



public class WayPointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] wayPoints;
    int currentWayPointIndex = 0;
    [SerializeField] float speed = 1f;


    void Start()
    {

    }
    void Update()
    {
        float fixedY = transform.position.y;

        Vector3 target = wayPoints[currentWayPointIndex].transform.position;
        target.y = fixedY;

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= wayPoints.Length)
                currentWayPointIndex = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }
}