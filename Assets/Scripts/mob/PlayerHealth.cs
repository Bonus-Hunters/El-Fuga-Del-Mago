using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    float hp;

    void Awake() => hp = maxHealth;

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        Debug.Log($"Player hit: -{dmg} hp. Remaining: {hp}");
        if (hp <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Player died.");
        // minimal: disable object
        gameObject.SetActive(false);
    }
}
