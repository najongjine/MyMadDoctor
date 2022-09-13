using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{

    Idle,
    Attack,
    Walk,
    Hit,
    Electric,
    Death

}

public class Enemy : MonoBehaviour
{
    private Transform playerTarget;

    [SerializeField]
    private float moveSpeed = 2f;

    private Vector3 tempScale;

    private float stoppingDistance;

    [SerializeField]
    private float attackerStoppingDistance = 1.5f, shooterStoppingDistance = 8f;

    [SerializeField]
    private bool isShooter;

    private PlayerAnimation enemyAnimation;

    [SerializeField]
    private float damageWaitTime = 0.5f;

    private float damageTimer;

    [SerializeField]
    private float attackWaitTime = 2.5f;

    private float attackerTimer;

    [SerializeField]
    private float attackFinishWaitTime = 0.5f;

    private float attackFinishedTimer;

    private EnemyState enemyState;

    

    [SerializeField]
    private EnemyDamageArea damageArea;
    
    [SerializeField]
    private EnemyBullet enemyBullet;
    
    [SerializeField]
    private Transform enemyBulletSpawnPos;
    
    private Health enemyHealth;

    private bool enemyDead;

    private void Awake()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        enemyAnimation = GetComponent<PlayerAnimation>();

        enemyHealth = GetComponent<Health>();

    }
    // Start is called before the first frame update
    void Start()
    {
        if (isShooter)
            stoppingDistance = shooterStoppingDistance;
        else
            stoppingDistance = attackerStoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDead)
            return;

        AnimateEnemy();
        SearchForPlayer();
    }
    void AnimateEnemy()
    {

        if (enemyState == EnemyState.Idle)
            enemyAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);

        if (enemyState == EnemyState.Attack)
            enemyAnimation.PlayAnimation(TagManager.ATTACK_ANIMATION_NAME);

        if (enemyState == EnemyState.Death)
        {
            enemyDead = true;
            enemyAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        }

        if (enemyState == EnemyState.Electric)
            enemyAnimation.PlayAnimation(TagManager.ELECTRIC_ANIMATION_NAME);

        if (enemyState == EnemyState.Hit)
            enemyAnimation.PlayAnimation(TagManager.HIT_ANIMATION_NAME);

        if (enemyState == EnemyState.Walk)
            enemyAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);

    }
    void SearchForPlayer()
    {

        if (enemyState == EnemyState.Death)
            return;

        if (!playerTarget)
        {
            enemyState = EnemyState.Idle;
            return;
        }

        if (enemyState == EnemyState.Hit)
        {
            CheckIfDamageIsOver();
            return;
        }

        if (enemyState == EnemyState.Electric) { 
            return;
        }

        if (Vector3.Distance(transform.position, playerTarget.position) > stoppingDistance)
        {

            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position,
                moveSpeed * Time.deltaTime);

            HandleFacingDirection();

            enemyState = EnemyState.Walk;

        }
        else
        {
            CheckIfAttackFinished();
            Attack();
        }

    }

    void HandleFacingDirection()
    {
        tempScale = transform.localScale;

        if (transform.position.x > playerTarget.position.x)
            tempScale.x = Mathf.Abs(tempScale.x);
        else
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;
    }
    void CheckIfAttackFinished()
    {
        if (Time.time > attackFinishedTimer)
        {
            enemyState = EnemyState.Idle;
        }
    }
    void Attack()
    {

        if (Time.time > attackerTimer)
        {
            // give attack animation time to play
            attackFinishedTimer = Time.time + attackFinishWaitTime;
            attackerTimer = Time.time + attackWaitTime;

            enemyState = EnemyState.Attack;

            if (isShooter)
            {
                // shoot the bullet
                var bulletObj=EnemyBulletPool.instance.GetBullet(enemyBullet);
                bulletObj.transform.position = new Vector2(enemyBulletSpawnPos.position.x, enemyBulletSpawnPos.position.y);
                if (transform.position.x > playerTarget.transform.position.x)
                {
                    /*
                    Instantiate(enemyBullet, enemyBulletSpawnPos.position,
                        Quaternion.identity).SetNegativeSpeed();
                    */
                    //enemyBulletPool.ShootBullet(enemyBulletSpawnPos.position, true);
                    bulletObj.SetNegativeSpeed();
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x),transform.localScale.y);
                }
                else
                {
                    /*
                    Instantiate(enemyBullet, enemyBulletSpawnPos.position,
                        Quaternion.identity);
                    */
                    //enemyBulletPool.ShootBullet(enemyBulletSpawnPos.position, false);
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }

            }

        } // if we can attack

    }
    void EnemyAttackerAttacked()
    {
        damageArea.gameObject.SetActive(true);
        damageArea.ResetDeactivateTimer();
    }
    void EnemyDamaged(bool electricDamage)
    {

        if (electricDamage)
        {
            enemyState = EnemyState.Electric;

            DealDamage(2);
        }
        else
        {

            damageTimer = Time.time + damageWaitTime;
            enemyState = EnemyState.Hit;

            DealDamage(1);
        }
    }
    void CheckIfDamageIsOver()
    {
        if (Time.time > damageTimer)
            enemyState = EnemyState.Idle;
    }

    void DealDamage(int amount)
    {
        enemyHealth.EnemyTakeDamage(amount);

        if (enemyHealth.GetEnemyHealth() <= 0)
        {

            GetComponent<Collider2D>().enabled = false;

            enemyState = EnemyState.Death;

            Invoke("RemoveEnemyFromGame", 2f);

            GameplayUIController.instance.SetKillScoreText();

        }
    }

    void RemoveEnemyFromGame()
    {
        // inform the spawner to remove the enemy
        EnemySpawner.instance.RemoveSpawnedEnemy(gameObject);
        Destroy(gameObject);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(TagManager.PLAYER_BULLET_TAG))
        {
            EnemyDamaged(false);
        }

        if (collision.CompareTag(TagManager.ELECTRIC_BULLET_TAG))
        {
            EnemyDamaged(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.ELECTRIC_BULLET_TAG))
        {
            enemyState = EnemyState.Idle;
        }
    }


}
