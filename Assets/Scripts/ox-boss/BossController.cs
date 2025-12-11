using System.Collections;
using UnityEngine;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;
using System;
public class BossController : MonoBehaviour, IAttackable
{
    #region Variables
    private float intervalAttack = 5.0f;
    private float dist;
    private float timer = 5.0f;
    private bool playChaseSound;
    private AnimatorStateInfo AnimatorInfo;
    [Header("Boss Stats")]
    private bool playerInRange = false;
    public float maxHealth = 300f;
    public float currentHealth = 300f;
    public float regenRate = 10f;

    [Header("Projectile Settings")]
    public float spawnRadius = 10f;
    public float spawnInterval = 1f;
    public float projectileSpeed = 4f;
    public int maxSimultaneous = 5;

    [Header("Player Detection")]
    public float detectionRange = 6f;
    [SerializeField] float ProjectileDamageAmount = 10f;
    [SerializeField] float AttackDamageAmount = 20f;
    bool isAttacking = false, isDead = false;
    #endregion

    #region References
    [Header("References")]
    public GameObject projectilePrefab, bossHealthUI;
    public Transform projectilesParent, player;
    public EnemyMovement movement;
    public Animator animator;
    EnemyZoneTrigger zoneTrigger;
    public Image healthFill;
    public AudioSource deathSound, ChasingSound, AttackSound, ProjectileSound;
    #endregion


    void Start()
    {
        StartCoroutine(Spawner());
        movement = GetComponent<EnemyMovement>();
        animator = GetComponentInChildren<Animator>();
        zoneTrigger = GetComponentInChildren<EnemyZoneTrigger>();
        Debug.Log(zoneTrigger);
        bossHealthUI.SetActive(false);
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(
                 gameObject.transform.position.x,
                 8.8f,
                 gameObject.transform.position.z
             );
        if (isDead)
            return;
        playerInRange = movement.isPlayerInAttackZone;
        AnimatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        HandleAnimations();
        DisplayHealthBar();
        HandleChasingSound();
        if (!movement.isPlayerInAttackZone)
            playChaseSound = true;
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

    }

    IEnumerator WaitForDeath(float length)
    {
        animator.Play("die");
        deathSound.Play();
        yield return new WaitForSeconds(length);
        DestroySelf();
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
        Vector2 rand = UnityEngine.Random.insideUnitCircle * spawnRadius;

        Vector3 target = new Vector3(
            transform.position.x + rand.x,
            transform.position.y - 20,
            transform.position.z + rand.y
        );

        // Spawn projectile
        GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectilesParent);

        // Use the generic Initialize method
        FireProjectile proj = go.GetComponent<FireProjectile>();
        proj.Initialize(target, projectileSpeed);
    }
    #endregion

    #region Sounds
    void HandleChasingSound()
    {
        if (movement.isPlayerInAttackZone && playChaseSound)
            StartCoroutine(WaitForChasing(ChasingSound.clip.length));
    }
    IEnumerator WaitForChasing(float length)
    {
        ChasingSound.Play();
        playChaseSound = false;
        yield return new WaitForSeconds(length);
    }
    #endregion

    #region Combat Trigger
    void OnTriggerStay(Collider other)
    {
        if (isDead)
            return;
        IAttackable gotHit = other.GetComponent<IAttackable>();

        timer += Time.deltaTime;
        if (timer >= intervalAttack)
        {
            // if enemy hit an attackabale object -> [player]
            if (gotHit != null)
            {
                gotHit.TakeDamage(ProjectileDamageAmount);
                ProjectileSound.Play();
            }
            timer = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead)
            return;
        animator.Play("attack_01");
        // AttackSound.Play();
        movement.isMoving = false;
        isAttacking = true;
        IAttackable gotHit = other.GetComponent<IAttackable>();
        if (gotHit != null)
            gotHit.TakeDamage(AttackDamageAmount);

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
        else
            currentHealth = maxHealth;
    }
    #endregion

    #region Death
    public void TakeDamage(float damage)
    {
        Debug.Log("OX boss got damaged");
        currentHealth -= damage;
        HandleDeath();
    }

    void HandleDeath()
    {
        if (currentHealth > 0)
            return;
        isDead = true;
        currentHealth = 0;
        Debug.Log("Boss is dead");
        StartCoroutine(WaitForDeath(Math.Max(deathSound.clip.length, AnimatorInfo.length)));

    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    #endregion
}
