using System.Collections;
using UnityEngine;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;
public class BossController : MonoBehaviour, IAttackable
{
    #region Variables
    private float intervalAttack = 5.0f;
    private float dist;
    private float timer = 5.0f;

    [Header("Boss Stats")]
    private bool playerInRange = false;
    public float maxHealth = 100f;
    public float currentHealth;
    public float regenRate = 2f;

    [Header("Projectile Settings")]
    public float spawnRadius = 10f;
    public float spawnInterval = 1f;
    public float projectileSpeed = 4f;
    public int maxSimultaneous = 5;

    [Header("Player Detection")]
    public float detectionRange = 6f;
    [SerializeField] float damageAmount = 10f;
    bool isAttacking = false, isDead = false;
    #endregion

    #region References
    [Header("References")]
    public GameObject projectilePrefab, bossHealthUI;
    public Transform projectilesParent, player;
    public EnemyMovement movement;
    public Animator animator;
    public Image healthFill;
    public AudioSource deathSound, ChasingSound, AttackSound;
    #endregion


    void Start()
    {
        StartCoroutine(Spawner());
        movement = GetComponent<EnemyMovement>();
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        bossHealthUI.SetActive(false);
    }

    void Update()
    {
        playerInRange = movement.isPlayerInAttackZone;
        HandleAnimations();
        DisplayHealthBar();
    }

    #region Animations
    void HandleAnimations()
    {
        if (!isDead && movement.isMoving)
        {
            if (movement.ChasingMove)
                animator.Play("run");
            else
                animator.Play("walk");
        }
        if (!isDead && !movement.isMoving && !isAttacking)
            animator.Play("idle");
        if (isDead)
        {
            animator.Play("defy");
        }
    }
    #endregion

    #region Projectiles
    IEnumerator Spawner()
    {
        while (true)
        {
            // Only attack if player is close
            if (player != null)
            {
                dist = Vector3.Distance(transform.position, player.position);
                if (dist <= detectionRange)
                    TrySpawnProjectile();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void TrySpawnProjectile()
    {
        if (maxSimultaneous > 0 && projectilesParent.childCount >= maxSimultaneous || isDead)
            return;

        // Pick random point around boss
        Vector2 rand = Random.insideUnitCircle * spawnRadius;

        Vector3 target = new Vector3(
            transform.position.x + rand.x,
            transform.position.y,
            transform.position.z + rand.y
        );

        // Spawn projectile
        GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectilesParent);

        // Use the generic Initialize method
        FireProjectile proj = go.GetComponent<FireProjectile>();
        proj.Initialize(target, projectileSpeed);
    }
    #endregion

    #region Combat Trigger
    void OnTriggerStay(Collider other)
    {
        if (isDead)
            return;
        Debug.Log("PLayer is still clliding with enemy");

        IAttackable gotHit = other.GetComponent<IAttackable>();

        timer += Time.deltaTime;
        if (timer >= intervalAttack)
        {
            // if enemy hit an attackabale object -> [player]
            if (gotHit != null)
                gotHit.TakeDamage(damageAmount);
            timer = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead)
            return;
        animator.Play("attack_01");
        movement.isMoving = false;
        isAttacking = true;
    }

    void OnTriggerExit(Collider other)
    {
        isAttacking = false;
        movement.isMoving = true;
        timer = 0f;

    }
    #endregion

    #region Health
    void DisplayHealthBar()
    {
        if (!playerInRange)
        {
            bossHealthUI.SetActive(false);
            RegenerateHealth();
            return;
        }
        bossHealthUI.SetActive(true);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float fill = currentHealth / maxHealth;
        healthFill.fillAmount = fill;
    }

    private void RegenerateHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += regenRate * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }
    #endregion

    #region Death
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HandleDeath();
    }

    void HandleDeath()
    {
        if (currentHealth > 0)
            return;
        currentHealth = 0;
        isDead = true;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    #endregion
}
