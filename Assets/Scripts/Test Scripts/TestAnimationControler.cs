using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationControler : MonoBehaviour
{
    public Animator playerAnimator;

    public GameObject animationEventGO;
    public TestAnimationEvent animationEvent;

    private void Start()
    {
        animationEvent = animationEventGO.GetComponent<TestAnimationEvent>();
    }

    public void testAttackButton()
    {
        animationEvent.enemyState = TestAnimationEvent.enemyStates.ALIVE;
        playerAnimator.SetTrigger("Attack");
    }

    public void testStrongAttackButton()
    {
        playerAnimator.SetTrigger("StrongAttack");
    }

    public void testDieButton()
    {
        animationEvent.enemyState = TestAnimationEvent.enemyStates.DEAD;
        playerAnimator.SetTrigger("Attack");
    }
}
