using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrcAttackSystem : MonoBehaviour
{
    public GameObject BattleSystemGO;
    public BattleSystem battleSystem;

    private void Start()
    {
        battleSystem = BattleSystemGO.GetComponent<BattleSystem>();
    }

    public IEnumerator EnemyTurn()
    {
        battleSystem.dialogText.text = battleSystem.enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        //Attack randomization

        int tempRandom = Random.Range(1, 100);

        if (battleSystem.enemyUnit.currentHP < battleSystem.enemyUnit.maxHP / 2 && tempRandom > 100 - battleSystem.orcHealChance)
        {
            //heal
            Debug.Log("Heal");
            battleSystem.enemyUnit.currentHP += 15;
        }
        else
        {
            tempRandom = Random.Range(1, 100);
            if (tempRandom > 100 - battleSystem.orcCritChance)
            {
                //critical hit
                battleSystem.isDead = battleSystem.playerUnit.TakeDamage(battleSystem.enemyUnit.damage * 3);
                battleSystem.dialogText.text = battleSystem.enemyUnit.unitName + " hits critical!";
                yield return new WaitForSeconds(1f);
            }
            else
            {
                //normal hit
                battleSystem.isDead = battleSystem.playerUnit.TakeDamage(battleSystem.enemyUnit.damage);
            }
        }

        battleSystem.enemyHUD.SetHP(battleSystem.enemyUnit.currentHP, battleSystem.enemyUnit);
        battleSystem.playerHUD.SetHP(battleSystem.playerUnit.currentHP, battleSystem.playerUnit);

        yield return new WaitForSeconds(1f);

        if (battleSystem.isDead)
        {
            battleSystem.state = BattleState.LOST;
            battleSystem.EndBattle();
        }
        else
        {
            battleSystem.state = BattleState.PLAYERTURN;
            battleSystem.PlayerTurn();
        }
    }
}
