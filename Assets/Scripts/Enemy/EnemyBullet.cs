using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 15f;

    private Vector3 moveVector;
    private Vector3 tempScale;

    [SerializeField]
    private bool playSound_1;

    private TrailRenderer trail;

    private void Awake()
    {
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        if (playSound_1)
        {
            //SoundManager.instance.Enemy_Weapon_1_Shoot();
        }
        else
        {
            //SoundManager.instance.Enemy_Weapon_2_Shoot();
        }
    }

    private void OnDisable()
    {

        moveVector = Vector3.zero;

        moveSpeed = Mathf.Abs(moveSpeed);

        tempScale = transform.localScale;
        tempScale.x = Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;

        trail.Clear();

    }

    private void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        moveVector.x = moveSpeed * Time.deltaTime;
        transform.position += moveVector;
    }

    public void SetNegativeSpeed()
    {
        moveSpeed = -Mathf.Abs(moveSpeed);

        tempScale = transform.localScale;
        tempScale.x = -Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (gameObject.CompareTag(TagManager.ENEMY_BULLET_TAG)
            && collision.CompareTag(TagManager.PLAYER_TAG))
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }
}
