using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField]
    private AudioSource weapon_2_AudioSource;

    [SerializeField]
    private AudioClip weapon_1_AudioClip, weapon_3_AudioClip, weapon_4_AudioClip;

    [SerializeField]
    private AudioClip[] playerHurtClips;

    [SerializeField]
    private AudioClip enemy_Weapon_1_AudioClip, enemy_Weapon_2_AudioClip;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Weapon_1_Shoot()
    {
        AudioSource.PlayClipAtPoint(weapon_1_AudioClip, transform.position);
    }

    public void Weapon_2_Shoot(bool playSound)
    {
        if (playSound)
        {
            if (!weapon_2_AudioSource.isPlaying)
            {
                weapon_2_AudioSource.Play();
            }
        }
        else
            weapon_2_AudioSource.Stop();
    }

    public void Weapon_3_Shoot()
    {
        AudioSource.PlayClipAtPoint(weapon_3_AudioClip, transform.position);
    }

    public void Weapon_4_Shoot()
    {
        AudioSource.PlayClipAtPoint(weapon_4_AudioClip, transform.position);
    }

    public void PlayerHurt()
    {
        AudioSource.PlayClipAtPoint(playerHurtClips[Random.Range(0, playerHurtClips.Length)],
            transform.position);
    }

    public void Enemy_Weapon_1_Shoot()
    {
        AudioSource.PlayClipAtPoint(enemy_Weapon_1_AudioClip, transform.position);
    }

    public void Enemy_Weapon_2_Shoot()
    {
        AudioSource.PlayClipAtPoint(enemy_Weapon_2_AudioClip, transform.position);
    }

} // class






































