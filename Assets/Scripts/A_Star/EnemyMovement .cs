using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    NavMeshAgent agent;
    private bool isPlayerInSafeZone = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isPlayerInSafeZone)
        {
            // Chase the player
            GetComponent<WayPointFollower>().enabled = false;
            agent.SetDestination(player.position);
        }
        else if (isPlayerInSafeZone)
        {
            // Stop chasing and return to patrol
            agent.ResetPath();
            GetComponent<WayPointFollower>().enabled = true;
        }
    }

    public void NotifyPlayerInZone(bool inZone)
    {
        isPlayerInSafeZone = inZone;
    }
}