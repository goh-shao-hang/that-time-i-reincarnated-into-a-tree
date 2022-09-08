using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected SpriteRenderer enemySprite;
    [SerializeField] protected int startingHealth;
    [SerializeField] protected int fearHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackRange;

    [SerializeField] protected int health;
    protected EnemyState currentState = EnemyState.Approaching;

    protected Transform player;
    protected Vector3 playerPosition;

    //Hit Effect
    [SerializeField]
    protected Material flashMaterial;
    protected Material originalMaterial;

    //Health Bar
    private Camera mainCam;
    private Slider healthBar;
    [SerializeField] private Color FearColor = Color.blue;
    [SerializeField] private Color HealthyColor = Color.green;
    [SerializeField] private Vector3 offset;

    public enum EnemyState
    {
        Empty,
        Approaching,
        Attacking,
        Running
    }

    protected virtual void Start()
    {
        health = startingHealth;
        enemySprite = GetComponent<SpriteRenderer>();
        originalMaterial = enemySprite.material;

        //Health Bar
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = startingHealth;
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);

        player = GameObject.Find("Player").GetComponent<Transform>();
        playerPosition = new Vector3(player.position.x, transform.position.y, transform.position.z); //Placed in start since player can't move
    }

    protected virtual void Update()
    {
        CalculateCurrentState();
        HandleCurrentState();
        UpdateHealthBarPosition();
    }

    bool InAttackRange()
    {
        if (Vector3.Distance(transform.position, playerPosition) > attackRange)
            return false;
        else 
            return true;
    }

    void CalculateCurrentState()
    {
        EnemyState requestedState = EnemyState.Empty;
        if (health > fearHealth && !InAttackRange())
        {
            requestedState = EnemyState.Approaching;
        }
        else if (health > fearHealth && InAttackRange())
        {
            requestedState = EnemyState.Attacking;
        }
        else if (health <= fearHealth)
        {
            requestedState = EnemyState.Running;
        }

        if (currentState != requestedState)
        {
            currentState = requestedState;
        }
    }

    void HandleCurrentState()
    {
        switch (currentState)
        {
            case EnemyState.Approaching:
                HandleApproaching();
                break;
            case EnemyState.Attacking:
                HandleAttacking();
                break;
            case EnemyState.Running:
                HandleRunning();
                break;
        }
    }

    protected virtual void HandleApproaching()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        if (transform.position.x > playerPosition.x)
            enemySprite.flipX = false;
        else
            enemySprite.flipX = true;
    }

    protected virtual void HandleAttacking() { }

    protected virtual void HandleRunning()
    {
        enemySprite.color = Color.blue;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, -speed * Time.deltaTime);
        if (transform.position.x > playerPosition.x)
            enemySprite.flipX = true;
        else
            enemySprite.flipX = false;
    }

    void UpdateHealthBarPosition()
    {
        healthBar.transform.position = mainCam.WorldToScreenPoint(transform.position + offset);
    }

    public void TakeDamage(int damage)
    {
        if (health >= fearHealth && (health - damage) <= 0) //Checks if an attack will kill a non-running enemy
        {
            health = 1;
        }
        else
            health -= damage;

        //Update Health Bar
        healthBar.gameObject.SetActive(health < startingHealth);
        healthBar.value = health;
        if (health <= fearHealth)
            healthBar.fillRect.GetComponentInChildren<Image>().color = FearColor;
        else
            healthBar.fillRect.GetComponentInChildren<Image>().color = HealthyColor;

        //Flash Effect
        StartCoroutine(Flash());
        if (health <= fearHealth)
        {
            if (health <= 0)
            {

            }
        }

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.enemiesKilled += 1;
    }

    IEnumerator Flash()
    {
        enemySprite.material = flashMaterial;
        yield return new WaitForSeconds(0.1f);
        enemySprite.material = originalMaterial;
    }
}
