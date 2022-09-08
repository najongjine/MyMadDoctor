using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D theRB;

    [SerializeField]
    float moveSpeed = 3.5f;

    [SerializeField]
    float minBound_X = -71f, maxBound_X = 71f, minBound_Y = -3.3f, maxBound_Y = 0f;

    Vector3 tempPos;
    float xAxis, yAxis;

    PlayerAnimation playerAnimation;

    [SerializeField]
    float shootWaitTime = 0.5f;
    float waitBeforeShooting;

    [SerializeField]
    float walkWaitTime = 0.3f;

    float waitBeforeWalking;
    bool canMove = true;

    PlayerShootingManager playerShootingManager;

    Health playerHealth;
    bool playerDied;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    float damageColorWaitTime = 0.1f;
    float damageColorTimer;
    bool playerDamaged;
    Color tempColor;

    [SerializeField]
    Animator playerHurtFX;
    private void Awake()
    {
        theRB=GetComponent<Rigidbody2D>();
        playerAnimation=GetComponent<PlayerAnimation>();
        playerShootingManager=GetComponent<PlayerShootingManager>();
        playerHealth=GetComponent<Health>();
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerDied)
        {
            return;
        }
        HandleMovement();
        HandleAnimation();
        FlipSprite();
        HandleShooting();
        CheckToMove();
        ChangeDamageColor();
    }

    void HandleMovement()
    {
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        if (!canMove)
        {
            return;
        }

        tempPos = transform.position;
        tempPos.x += xAxis * moveSpeed * Time.deltaTime;
        tempPos.y += yAxis * moveSpeed * Time.deltaTime;

        if (tempPos.x < minBound_X)
        {
            tempPos.x = minBound_X;
        }
        if (tempPos.x > maxBound_X)
        {
            tempPos.x = maxBound_X;
        }
        if (tempPos.y < minBound_Y)
        {
            tempPos.y = minBound_Y;
        }
        if (tempPos.y > maxBound_Y)
        {
            tempPos.y = maxBound_Y;
        }
        transform.position = tempPos;
    }
    private void FlipSprite()
    {
        if (xAxis>0)
        {
            playerAnimation.ChangeFacingDirection(true);
        }
        if (xAxis < 0)
        {
            playerAnimation.ChangeFacingDirection(false);
        }
    }

    private void HandleAnimation()
    {
        if (!canMove)
        {
            return;
        }
        if (Mathf.Abs(xAxis)>0 || Mathf.Abs(yAxis) >0)
        {
            playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
        }
        else
        {
            playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }
    
    void StopMovement(float waitTime)
    {
        canMove = false;
        waitBeforeWalking = Time.time + waitTime;
    }
    void CheckToMove()
    {
        if (Time.time > waitBeforeWalking)
        {
            canMove = true;
        }
    }
    void HandleShooting()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    if (Time.time > waitBeforeShooting)
        //    {
        //        Shoot();
        //        playerShootingManager.ShootBullet(transform.localScale.x);
        //    }

        //}
        // if the current weapon is the electric gun
        if (playerShootingManager.GetWeaponType() == 1)
        {

            if (Input.GetButton("Fire1"))
            {
                playerShootingManager.ShootElectricity(true);
                Shoot();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                playerShootingManager.ShootElectricity(false);
                waitBeforeShooting = 0f;
                canMove = true;
            }

        }
        else
        {

            if (Input.GetButtonDown("Fire1"))
            {
                if (Time.time > waitBeforeShooting)
                {
                    playerShootingManager.ShootBullet(transform.localScale.x);
                    Shoot();
                }
            }

        }

    }
    void Shoot()
    {
        waitBeforeShooting = Time.time + shootWaitTime;
        StopMovement(walkWaitTime);
        playerAnimation.PlayAnimation(TagManager.SHOOT_ANIMATION_NAME);
    }
    public void TakeDamage(float amount)
    {

        if (playerDied)
            return;

        playerHealth.PlayerTakeDamage(amount);

        if (playerHealth.GetPlayerHealth() <= 0)
        {
            playerDied = true;

            playerAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);

            Invoke("RemovePlayerFromGame", 3f);

        }
        else
        {
            PlayerReceivedDamage();
        }

    }

    void RemovePlayerFromGame()
    {
        // call game over panel
        //GameOverUIController.instance.GameOver();
        Destroy(gameObject);
    }

    void PlayerReceivedDamage()
    {
        if (!playerDamaged)
        {

            tempColor = spriteRenderer.material.color;

            tempColor.a = 1f;
            tempColor.r = 1f;
            tempColor.g = 0f;
            tempColor.b = 0f;

            spriteRenderer.material.SetColor("_Color", tempColor);

            damageColorTimer = Time.time + damageColorWaitTime;

            playerDamaged = true;

            playerHurtFX.Play(TagManager.FX_ANIMATION_NAME);

            // play the hurt sound fx
            //SoundManager.instance.PlayerHurt();
        }
    }

    // change back to original color
    void ChangeDamageColor()
    {

        if (playerDamaged)
        {

            if (Time.time > damageColorTimer)
            {

                playerDamaged = false;

                tempColor = spriteRenderer.material.color;

                tempColor.a = 1f;
                tempColor.r = 1f;
                tempColor.g = 1f;
                tempColor.b = 1f;

                spriteRenderer.material.SetColor("_Color", tempColor);

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(TagManager.ENEMY_BULLET_TAG))
        {
            TakeDamage(20f);
        }

        if (collision.CompareTag(TagManager.HEALTH_FUEL_TAG))
        {
            //playerHealth.IncreaseHealth(collision.GetComponent<HealthFuel>().GetHealthValue());
            Destroy(collision.gameObject);
        }

    }

}
