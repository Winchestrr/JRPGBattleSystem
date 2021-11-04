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

    public void heroAttackAnim()
    {
        heroAnimator.SetTrigger("attack");
    }

    public void enemyAttackAnim()
    {
        enemyAnimator.SetTrigger("attack");
    }

    public void hurtAnim(string target)
    {
        if(target == "hero")
        {
            heroAnimator.SetTrigger("hurt");
        }
        else
        {
            enemyAnimator.SetTrigger("hurt");
        }
        
    }

    public void healAnim(string name)
    {
        if(name == "hero")
        {
            Instantiate(healAnimGO, battleSystem.playerBattleStation);
        }
        else
        {
            Instantiate(healAnimGO, battleSystem.enemyBattleStation);
        }

        healAnimator = healAnimGO.GetComponent<Animator>();
    }

    public void dieAnim(string target)
    {
        if (target == "hero")
        {
            heroAnimator.SetTrigger("die");
        }
        else
        {
            enemyAnimator.SetTrigger("die");
        }
    }    
}
