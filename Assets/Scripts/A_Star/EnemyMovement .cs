using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    NavMeshAgent agent;
    WayPointFollower wayPoint;
    public bool isPlayerInAttackZone = false;

    public bool ChasingMove = false;
    public bool isMoving = true;

    void Start()
    {
        wayPoint = GetComponent<WayPointFollower>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isMoving)
            return;
        if (isPlayerInAttackZone)
        {
            // Chase the player
            wayPoint.StopWalking();
            agent.SetDestination(player.position);
            ChasingMove = true;
        }
        else
        {
            // Stop chasing and return to patrol
            ChasingMove = false;
            agent.ResetPath();
            wayPoint.ResumeWalking();
        }
    }

    public void NotifyPlayerInZone(bool inZone)
    {
        isPlayerInAttackZone = inZone;
    }

    public void checkFirAttacks(bool shouldAttack)
    {
        if (shouldAttack)
        {
            wayPoint.isWalking = false;
            isMoving = false;
        }
    }
}