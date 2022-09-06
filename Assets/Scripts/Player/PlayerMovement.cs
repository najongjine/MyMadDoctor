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

    private void Awake()
    {
        theRB=GetComponent<Rigidbody2D>();
        playerAnimation=GetComponent<PlayerAnimation>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAnimation();
        FlipSprite();
    }

    void HandleMovement()
    {
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

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
        if (Mathf.Abs(xAxis)>0 || Mathf.Abs(yAxis) >0)
        {
            playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
        }
        else
        {
            playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }

}
