using UnityEngine;
using System.Collections.Generic;
using System;


public class Movement : MonoBehaviour
{
    private int currentIndex = 0;
    private float waypointTolerance;
    private float speed;
    GridManager grid;
    public List<Node> path;
    void Start()
    {
        grid = GetComponent<GridManager>();
    }
    void Update()
    {
        path = grid.path;
        if (path == null || path.Count == 0) return;

        Vector3 targetPos = path[currentIndex].worldPosition;
        Vector3 moveDir = (targetPos - transform.position).normalized;
        transform.position += moveDir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPos) < waypointTolerance)
        {
            currentIndex++;
            if (currentIndex >= path.Count)
            {
                currentIndex = path.Count - 1;
            }
        }
    }

    public void ResetPath()
    {
        currentIndex = 0;
    }
}
