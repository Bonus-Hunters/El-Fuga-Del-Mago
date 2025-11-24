using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    NavMeshAgent agent;
    WayPointFollower wayPoint;
    private bool isPlayerInAttackZone = false;


    void Start()
    {
        wayPoint = GetComponent<WayPointFollower>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isPlayerInAttackZone)
        {
            // Chase the player
            wayPoint.StopWalking();
            agent.SetDestination(player.position);
        }
        else
        {
            // Stop chasing and return to patrol
            agent.ResetPath();
            wayPoint.ResumeWalking();
        }
    }

    public void NotifyPlayerInZone(bool inZone)
    {
        isPlayerInAttackZone = inZone;
    }
}