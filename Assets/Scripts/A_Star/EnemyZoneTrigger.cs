using UnityEngine;


public class EnemyZoneTrigger : MonoBehaviour
{

    public EnemyMovement enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.NotifyPlayerInZone(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enemy.NotifyPlayerInZone(false);

    }
}