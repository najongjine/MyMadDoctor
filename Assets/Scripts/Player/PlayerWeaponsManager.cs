using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{

    private PlayerAnimation playerAnimation;

    private int weaponIndex;

    private int numberOfWeapons;

    private PlayerShootingManager playerShootingManager;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();

        numberOfWeapons = playerAnimation.GetNumberOfWeapons();

        weaponIndex = 0;

        playerAnimation.ChangeAnimatorController(weaponIndex);

        playerShootingManager = GetComponent<PlayerShootingManager>();
        playerShootingManager.SetWeaponType(weaponIndex);

    }

    private void Update()
    {
        ChangeWeapon();
    }

    void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            weaponIndex++;

            if (weaponIndex == numberOfWeapons)
                weaponIndex = 0;

            playerAnimation.ChangeAnimatorController(weaponIndex);

            playerShootingManager.SetWeaponType(weaponIndex);
            GameplayUIController.instance.ChangeWeaponIcon();

        }
    }


} // class



































