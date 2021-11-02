using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControler : MonoBehaviour
{
    public BattleSystem battleSystem;

    public Animator heroAnimator;
    public static Animator enemyAnimator;

    private void Start()
    {
        heroAnimator = battleSystem.playerGO.GetComponent<Animator>();
        enemyAnimator = battleSystem.enemyGO.GetComponent<Animator>();
    }

    public void heroAttackAnim()
    {
        heroAnimator.SetTrigger("attack");
    }

    public void enemyAttackAnim()
    {
        enemyAnimator.SetTrigger("attack");
    }

    public static void hurtAnim()
    {
        enemyAnimator.SetTrigger("hurt");
    }
}
