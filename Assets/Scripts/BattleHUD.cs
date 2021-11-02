using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Text hpText;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;

        hpText.text = unit.maxHP + " / " + unit.maxHP;
    }

    public void SetHP(int hp, Unit unit)
    {
        hpSlider.value = hp;

        hpText.text = hp + " / " + unit.maxHP;
    }
}
