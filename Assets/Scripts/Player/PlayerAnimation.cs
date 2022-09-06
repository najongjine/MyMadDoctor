using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    Vector3 currentScale;
    string currentAnimation;

    [SerializeField]
    private RuntimeAnimatorController[] animControllers;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAnimation(string newAnimation)
    {
        if(currentAnimation == newAnimation)
        {
            return;
        }
        anim.Play(newAnimation);
        currentAnimation = newAnimation;
    }
    public void ChangeFacingDirection(bool faceRight)
    {
        currentScale = transform.localScale;
        if (faceRight)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        else
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }
        transform.localScale = currentScale;
    }
    public void ChangeAnimatorController(int controllerIndex)
    {
        anim.runtimeAnimatorController = animControllers[controllerIndex];

        currentAnimation = "";
    }
    public int GetNumberOfWeapons()
    {
        return animControllers.Length;
    }

}
