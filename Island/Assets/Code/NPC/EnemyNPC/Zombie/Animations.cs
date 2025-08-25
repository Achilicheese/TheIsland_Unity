using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Animations : MonoBehaviour
{
    private Animator zombieAnimator;
    private movements currentAnimation;
    private NavMeshAgent agent;

    public enum movements
    {
        idle,
        walk
    }

    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        currentAnimation = movements.idle;
        CrossFadeAnimation("Idle");
    }

    void Update()
    {
        UpdateAnimationBasedOnMovement();
    }

    private void UpdateAnimationBasedOnMovement()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            SetAnimation(movements.walk);
        }
        else
        {
            SetAnimation(movements.idle);
        }
    }

    public void SetAnimation(movements newAnimation)
    {
        if (currentAnimation != newAnimation)
        {
            currentAnimation = newAnimation;
            changeMovementsAnimations();
        }
    }

    private void changeMovementsAnimations()
    {
        switch (currentAnimation)
        {
            case movements.idle:
                CrossFadeAnimation("Idle");
                break;
            case movements.walk:
                CrossFadeAnimation("Walk");
                break;
        }
    }

    private void CrossFadeAnimation(string typeOfAnimation)
    {
        if (zombieAnimator != null)
        {
            zombieAnimator.CrossFade(typeOfAnimation, 0.1f);
        }
    }
}