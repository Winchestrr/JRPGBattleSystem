using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject damageTextGO;
    public Text damageText;

    public BattleSystem battleSystem;
    public AnimationControler animationControler;

    public void VisualizePlayerDamage(int amount, string type)
    {
        Debug.Log("instantiated text");

        damageTextGO = Instantiate(damageTextPrefab);
        damageText = damageTextGO.GetComponent<Text>();
        damageText.text = amount.ToString();

        switch (type)
        {
            case "damage":
                damageText.color = Color.red;
                break;

            case "heal":
                damageText.color = Color.green;
                break;
        }

        //animationControler.DamageTextAnim();
    }
}
