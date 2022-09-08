using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 15f;
    Vector3 moveVector = Vector3.zero;
    Vector3 tempScale;

    [SerializeField]
    private bool getTrailRenderer;

    private TrailRenderer trail;

    private void Awake()
    {
        if (getTrailRenderer)
            trail = transform.GetComponentInChildren<TrailRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // on disable is called when obj is SetActive(false);
    private void OnDisable()
    {

        moveVector = Vector3.zero;

        moveSpeed = Mathf.Abs(moveSpeed);

        tempScale = transform.localScale;
        tempScale.x = Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;

        if (getTrailRenderer)
            trail.Clear();
    }

    // Update is called once per frame
    void Update()
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
        tempScale.x= -Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag==TagManager.ENEMY_TAG)
        {
            gameObject.SetActive(false);
        }
    }

}
