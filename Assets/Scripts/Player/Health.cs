using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int enemyHealth = 5;

    [SerializeField]
    private float playerMaxHealth = 100f;

    private float playerHealth;

    private void Start()
    {
        playerHealth = playerMaxHealth;

        if (gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            // initialize health slider
            //GameplayUIController.instance.InitializeHealthSlider(0, playerHealth, playerHealth);
        }

    }

    public void EnemyTakeDamage(int amount)
    {
        enemyHealth -= amount;
    }

    public int GetEnemyHealth()
    {
        return enemyHealth;
    }

    public void PlayerTakeDamage(float amount)
    {
        playerHealth -= amount;

        // preview health UI
        //GameplayUIController.instance.SetHealthSliderValue(playerHealth);

    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    public void IncreaseHealth(float amount)
    {
        playerHealth += amount;

        if (playerHealth > playerMaxHealth)
            playerHealth = playerMaxHealth;

        // inform health UI about health change
        //GameplayUIController.instance.SetHealthSliderValue(playerHealth);
    }

}
