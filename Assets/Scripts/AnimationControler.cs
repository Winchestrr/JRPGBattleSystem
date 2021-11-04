using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControler : MonoBehaviour
{
    public BattleSystem battleSystem;

    public Animator heroAnimator;
    public static Animator enemyAnimator;

    public GameObject healAnimGO;
    public Animator healAnimator;



    private void Start()
    {
        heroAnimator = battleSystem.playerGO.GetComponent<Animator>();
        enemyAnimator = battleSystem.enemyGO.GetComponent<Animator>();
    }

    public void AnimDestroy()
    {

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

    public void healAnim()
    {
        Instantiate(healAnimGO, battleSystem.playerBattleStation);
        healAnimator = healAnimGO.GetComponent<Animator>();
    }
}
