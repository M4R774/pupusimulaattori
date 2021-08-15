using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Made this into its own component so both Animal and AnimalAI can easily use it
//
public enum AnimationParameters
{
    isWalking,
    isIdle,
    isLove,
    isDead
}

public class AnimalAnimation : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] string currentAnimation = null; // We use this to deactive bools when switching animations
    
    void Start()
    {
        if(animator == null)
            animator = GetComponentInChildren<Animator>();
        currentAnimation = AnimationParameters.isIdle.ToString();
        animator.SetBool(currentAnimation, true);
    }

    public void SetAnimation(AnimationParameters animPar)
    {
        if(animPar.ToString() != currentAnimation)
        {
            animator.SetBool(currentAnimation, false);
            currentAnimation = animPar.ToString();
            animator.SetBool(currentAnimation, true);
        }
        // to do: implement death animation triggering, so other animations don't get called after death
    }
}
