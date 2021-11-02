using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSystem : MonoBehaviour
{
    public enum menuState { MAINMENU, ATTACKMENU, SPELLMENU };
    public menuState actualMenuState;

    public GameObject sfxManagerGO;
    private AudioManager sfxManager;

    public Text menuStateText;
    public Text gameStateText;

    public GameObject attackMenu;
    public GameObject spellMenu;

    public string pressedButton;

    public GameObject battleSystemGO;
    private BattleSystem battleSystem;

    public void Start()
    {
        actualMenuState = menuState.MAINMENU;
        battleSystem = battleSystemGO.GetComponent<BattleSystem>();

        sfxManager = sfxManagerGO.GetComponent<AudioManager>();
    }

    public void Update()
    {
        menuStateText.text = actualMenuState.ToString();
        gameStateText.text = battleSystem.state.ToString();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            actualMenuState = menuState.MAINMENU;
            attackMenu.SetActive(false);
            spellMenu.SetActive(false);
        }
    }

    public void onClickSound()
    {
        sfxManager.playSound("click");
    }

    public void onAttackButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN) return;

        switch (actualMenuState)
        {
            case menuState.MAINMENU:
                actualMenuState = menuState.ATTACKMENU;
                attackMenu.SetActive(true);
                break;

            case menuState.ATTACKMENU:
                actualMenuState = menuState.MAINMENU;
                attackMenu.SetActive(false);
                break;

            case menuState.SPELLMENU:
                actualMenuState = menuState.ATTACKMENU;
                spellMenu.SetActive(false);
                attackMenu.SetActive(true);
                break;
        }
    }

    public void onSpellButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN) return;

        switch (actualMenuState)
        {
            case menuState.MAINMENU:
                actualMenuState = menuState.SPELLMENU;
                spellMenu.SetActive(true);
                break;

            case menuState.SPELLMENU:
                actualMenuState = menuState.MAINMENU;
                spellMenu.SetActive(false);
                break;

            case menuState.ATTACKMENU:
                actualMenuState = menuState.SPELLMENU;
                attackMenu.SetActive(false);
                spellMenu.SetActive(true);
                break;
        }    
    }

    public void onRunAwayButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN) return;

        battleSystem.StartCoroutine("PlayerRunAway");
    }

    public void onSecondButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN) return;

        pressedButton = EventSystem.current.currentSelectedGameObject.name;

        actualMenuState = menuState.MAINMENU;
        attackMenu.SetActive(false);
        spellMenu.SetActive(false);

        switch (pressedButton)
        {
            case "Attack1":
                Debug.Log("Pressed Attack1");

                battleSystem.StartCoroutine("PlayerAttack");

                break;

            case "Attack2":
                Debug.Log("Pressed Attack2");
                break;

            case "Attack3":
                Debug.Log("Pressed Attack3");
                break;

            case "Spell1":
                Debug.Log("Pressed Spell1");

                battleSystem.StartCoroutine("PlayerHeal");

                break;

            case "Spell2":
                Debug.Log("Pressed Spell2");
                break;

            case "Spell3":
                Debug.Log("Pressed Spell3");
                break;
        }
    }
}
