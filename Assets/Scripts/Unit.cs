using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject animationControlerGO;
    public AnimationControler animationControler;

    public string unitType;

    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    private void Start()
    {
        animationControlerGO = GameObject.Find("AnimationControler");
        animationControler = animationControlerGO.GetComponent<AnimationControler>();
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if(currentHP <= 0)
        {
            animationControler.dieAnim(unitType);

            currentHP = 0;
            return true;
        }
        else
        {
            animationControler.hurtAnim(unitType);

            return false;
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}
