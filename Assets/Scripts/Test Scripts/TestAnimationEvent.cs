using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationEvent : MonoBehaviour
{
    public enum enemyStates { ALIVE, DEAD };
    public enemyStates enemyState;

    public Animator skeletonAnimator;

    public void PrintEvent()
    {
        Debug.Log("test");
    }

    public void DealDamage()
    {
        if(enemyState == enemyStates.ALIVE)
        {
            skeletonAnimator.SetTrigger("TakeDamage");
        }
        else
        {
            skeletonAnimator.SetTrigger("Die");
        }
    }
}
