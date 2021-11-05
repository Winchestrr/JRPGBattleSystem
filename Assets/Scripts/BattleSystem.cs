using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, BETWEENROUNDS };

public class BattleSystem : MonoBehaviour
{
    public GameObject playerGO;
    public GameObject enemyGO;

    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;

    public GameObject sfxManagerGO;
    private AudioManager sfxManager;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Unit playerUnit;
    public Unit enemyUnit;

    public Text dialogText;
    public GameObject randomizerTextGO;
    public Text randomizerText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public AnimationControler animationControler;
    public DamageText damageText;

    public BattleState state;

    public int orcHealChance;
    public int orcCritChance;
    public int playerMissChance;
    public int playerCritChance;
    public int playerRunAwayChance;

    public int randomIterations;
    private int tempInt;

    private int enemyRandom;

    private int tempDamage;
    public bool isDead = false;

    void Awake()
    {
        sfxManager = sfxManagerGO.GetComponent<AudioManager>();

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerGO = (GameObject)Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        enemyRandom = Random.Range(0, 2);
        Debug.Log(enemyRandom);

        //enemyGO = (GameObject)Instantiate(enemyPrefabs[enemyRandom], enemyBattleStation);
        enemyGO = (GameObject)Instantiate(enemyPrefabs[0], enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogText.text = enemyUnit.unitName + " wants to fight!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public IEnumerator PlayerAttack()
    {
        int tempRandom = Random.Range(1, 100);
        if(tempRandom < playerMissChance)
        {
            //miss
            dialogText.text = "You missed the attack";
        }
        else if(tempRandom > 100 - playerCritChance)
        {
            //critical hit
            sfxManager.playSound("smash");

            tempDamage = playerUnit.damage  * 2;
            isDead = enemyUnit.TakeDamage(tempDamage);
            enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit);
            dialogText.text = "The attack is succesful!\nYou hit " + enemyUnit.unitName + " for " + playerUnit.damage * 2 + "HP. The hit was critical.";
        }    
        else
        {
            //casual hit
            sfxManager.playSound("sword");

            isDead = enemyUnit.TakeDamage(playerUnit.damage);

            enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit);
            dialogText.text = "The attack is succesful!\nYou hit " + enemyUnit.unitName + " for " + playerUnit.damage + "HP.";
        }



        state = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            Debug.Log("test");
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        sfxManager.playSound("heal");
        animationControler.healAnim("hero");


        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP, playerUnit);
        dialogText.text = playerUnit.unitName + " healed himself for 5HP.";

        state = BattleState.ENEMYTURN;

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }


    public IEnumerator EnemyTurn()
    {
        dialogText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        //Attack randomization

        int tempRandom = Random.Range(1, 100);

        if(enemyUnit.currentHP < enemyUnit.maxHP/2 && tempRandom > 100 - orcHealChance)
        {
            //heal
            animationControler.healAnim("enemy");

            sfxManager.playSound("heal");
            Debug.Log("Heal");
            enemyUnit.currentHP += 15;
            dialogText.text = enemyUnit.unitName + " heals himself for 15HP.";
            yield return new WaitForSeconds(1f);
        }
        else
        {
            animationControler.enemyAttackAnim();

            tempRandom = Random.Range(1, 100);
            if(tempRandom > 100 - orcCritChance)
            {
                //critical hit
                sfxManager.playSound("smash");
                isDead = playerUnit.TakeDamage(enemyUnit.damage * 3);

                damageText.VisualizePlayerDamage(enemyUnit.damage * 3, "damage");

                dialogText.text = enemyUnit.unitName + " hits critical!";
                yield return new WaitForSeconds(1f);
            }
            else
            {
                //normal hit
                sfxManager.playSound("bonk");
                isDead = playerUnit.TakeDamage(enemyUnit.damage);

                damageText.VisualizePlayerDamage(enemyUnit.damage, "damage");
            }  
        }

        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit);
        playerHUD.SetHP(playerUnit.currentHP, playerUnit);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogText.text = "You won!\nYou gained 0XP :c";
        }
        else if(state == BattleState.LOST)
        {
            dialogText.text = "You lost!"; 
        }
    }

    public void PlayerTurn()
    {
        dialogText.text = "Choose an action, " + playerUnit.unitName + ":";
    }

    public void OnAttackButton()
    { 
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());

    }
    
    public void OnHealButton()
    { 
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

    public void PlayerAttackAnimation()
    {
        //.transform.position += new Vector3(10, 0, 0);
    }

    public IEnumerator PlayerRunAway()
    {
        randomizerTextGO.SetActive(true);

        for (int i = 0; i < randomIterations; i++)
        {
            randomizerText.text = Random.Range(1, 100).ToString();
            yield return new WaitForSeconds(0.05f);
        }

        tempInt = Random.Range(1, 100);

        if (tempInt > 100 - playerRunAwayChance)
        {
            state = BattleState.BETWEENROUNDS;

            randomizerText.color = Color.green;
            randomizerText.text = tempInt.ToString();
            Debug.Log("run away");

            if (tempInt > 90)
            {
                randomizerText.text += "!";
            }

            dialogText.text = playerUnit.unitName + " escaped succesfuly.";
        }
        else
        {
            state = BattleState.BETWEENROUNDS;

            randomizerText.color = Color.red;
            randomizerText.text = tempInt.ToString();
            Debug.Log("lost");

            dialogText.text = "Escape failed.";

            yield return new WaitForSeconds(1);

            randomizerText.color = Color.black;
            randomizerTextGO.SetActive(false);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void playerShield()
    {

    }
}
